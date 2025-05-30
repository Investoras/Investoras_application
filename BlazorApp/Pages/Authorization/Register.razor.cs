using ClassLibrary.Dto.User;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorApp.Models.User;


namespace BlazorApp.Pages.Authorization
{
    public partial class Register
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private CreateUserModel RegisterModel = new();
        private string? registerError;

        private async Task HandleRegister()
        {
            registerError = string.Empty;
            //RegisterModel.CreatedAt = DateTime.UtcNow;
            var response = await Http.PostAsJsonAsync("User", RegisterModel);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                registerError = "Ошибка регистраии.";
            }
        }
    }
}

