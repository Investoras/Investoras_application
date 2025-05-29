using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using BlazorApp.Models.User;
using BlazorApp.Mappings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorApp.Pages.User
{
    public partial class CreateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        public CreateUserModel UserData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public bool IsSubmitting { get; set; } = false;

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
                    ServerErrors.Add("Update error.");
                }
            }
            catch
            {
                ServerErrors.Add("Update error.");
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
