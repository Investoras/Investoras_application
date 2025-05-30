using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using BlazorApp.Models.User;
using BlazorApp.Mappings;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BlazorApp.Services;

namespace BlazorApp.Pages.User
{
    public partial class CreateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private int? UserId { get; set; }
        // FOR TESTING - TO ACCESS USER MANAGEMENT FUNCTION
        private readonly bool Superuser = false;

        public CreateUserModel UserData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public bool IsSubmitting { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (!Superuser)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                UserId = AuthService.GetUserId();
            }
            catch
            {
                NavigationManager.NavigateTo("/login");
                return;
            }
        }

        protected async Task SaveUser()
        {
            ServerErrors.Clear();
            IsSubmitting = true;

            try
            {
                var response = await Http.PostAsJsonAsync("User", UserData);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/Users");
                }
                else
                {
                    ServerErrors.Add("Проверьте правильность введённых данных.");
                }
            }
            catch
            {
                ServerErrors.Add("Ошибка операции.");
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
