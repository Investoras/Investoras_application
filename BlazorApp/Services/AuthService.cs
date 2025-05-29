using ClassLibrary.Dto;
using ClassLibrary.Dto.User;
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

    public async Task<bool> LoginAsync(LoginUserDto loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync("User/login", loginModel);

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

    public async Task RegisterAsync(CreateUserDto createUserDto)
    {
        var response = await _httpClient.PostAsJsonAsync("User", createUserDto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {error}");
        }

        Console.WriteLine("Регистрация успешна");
    }
}
