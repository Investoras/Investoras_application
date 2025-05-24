using ClassLibrary.Dto.User;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace BlazorApp.Pages.Authorization
{
    public partial class Register
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private UserDto RegisterModel = new();

        private async Task HandleRegister()
        {
            RegisterModel.CreatedAt = DateTime.UtcNow;
            var response = await Http.PostAsJsonAsync("https://localhost:7214/User", RegisterModel);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                // failed ???
            }
        }
    }
}

