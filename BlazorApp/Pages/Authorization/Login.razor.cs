using Microsoft.AspNetCore.Components;
using BlazorApp.Models.User;
using BlazorApp.Services;
using System.Net.Http.Json;



namespace BlazorApp.Pages.Authorization
{
    public partial class Login
    {
        [Inject] private IAuthService AuthService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private LoginUserModel loginModel = new();
        private string? loginError;

        private async Task HandleLogin()
        {
            loginError = string.Empty;
            if (await AuthService.LoginAsync(loginModel))
            {
                Navigation.NavigateTo("/Main");
            }
            else
            {
                loginError = "Неверное имя пользователя или пароль.";
            }
        }
    }
}
