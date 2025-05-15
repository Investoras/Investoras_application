using ClassLibrary.Dto;
using System.Net.Http.Json;

namespace BlazorApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    // Состояние аутентификации
    public bool IsAuthenticated { get; private set; }
    public int? UserId { get; private set; }

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(UserDto loginModel)
    {
        loginModel.Email = "";
        // Отправляем запрос на сервер для проверки учетных данных
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7214/User/login", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            UserId = user.UserId;
            IsAuthenticated = true;
            return true;
        }

        return false;
    }

    public Task LogoutAsync()
    {
        UserId = null;
        IsAuthenticated = false;
        return Task.CompletedTask;
    }

    public async Task RegisterAsync(UserDto loginModel)
    {
        // Отправляем запрос на сервер для проверки учетных данных
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7214/User", loginModel);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Регистрация успешна");
        }
    }
}