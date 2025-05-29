using ClassLibrary.Dto;
using BlazorApp.Models.User;
using BlazorApp.Mappings;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public bool IsAuthenticated { get; private set; }
    public int? UserId { get; private set; }
    public string? Token { get; private set; }

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
                UserId = authResponse.UserId;
                IsAuthenticated = true;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);

                return true;
            }
        }

        return false;
    }

    public Task LogoutAsync()
    {
        Token = null;
        UserId = null;
        IsAuthenticated = false;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return Task.CompletedTask;
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

        Console.WriteLine("Регистрация успешна");
    }
}
