using ClassLibrary.Dto;
using BlazorApp.Models.User;
using BlazorApp.Mappings;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace BlazorApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public bool IsAuthenticated { get; private set; }
    public int? UserId { get; private set; }
    public string? Username { get; private set; }
    public string? Token { get; private set; }

    public event Action? OnAuthStateChanged;

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public int GetUserId()
    {
        return UserId ?? throw new InvalidOperationException("Пользователь не авторизован.");
    }

    public async Task<bool> LoginAsync(LoginUserModel loginModel)
    {
        var loginDto = loginModel.ToDto();

        var response = await _httpClient.PostAsJsonAsync("User/login", loginDto);

        if (response.IsSuccessStatusCode)
        {
            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

            if (authResponse is not null)
            {
                Token = authResponse.Token;
                Username = authResponse.Username;
                UserId = authResponse.UserId;
                IsAuthenticated = true;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);

                await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "token", Token);
                await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "username", Username);
                await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "userId", UserId.ToString());

                OnAuthStateChanged?.Invoke();
                return true;
            }
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        OnAuthStateChanged?.Invoke();
        Username = null;
        Token = null;
        UserId = null;
        IsAuthenticated = false;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", "token");
        await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", "username");
        await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", "userId");
    }

    public async Task RegisterAsync(CreateUserModel createUserModel)
    {
        var createUserDto = createUserModel.ToDto();

        var response = await _httpClient.PostAsJsonAsync("User", createUserDto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {error}");
        }
    }

    public async Task TryRestoreSessionAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "token");
        var username = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "username");
        var userIdString = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "userId");

        if (!string.IsNullOrEmpty(token) &&
            !string.IsNullOrEmpty(username) && 
            int.TryParse(userIdString, out var userId))
        {
            Token = token;
            Username = username;
            UserId = userId;
            IsAuthenticated = true;

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);

            OnAuthStateChanged?.Invoke();
        }
    }
}
