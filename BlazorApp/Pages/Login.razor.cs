using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.User;
using BlazorApp.Services;
using System.Net.Http.Json;



namespace BlazorApp.Pages
{
    public partial class Login 
    {
        [Inject] private IAuthService AuthService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private LoginUserDto loginModel = new();
        private string? loginError;

        private async Task HandleLogin()
        {
            if (await AuthService.LoginAsync(loginModel))
            {
                Navigation.NavigateTo("/Users");
            }
            else
            {
                loginError = "Invalid username or password.";
            }
        }
    }
}
