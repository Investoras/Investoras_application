using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto;
using System.Net.Http.Json;


namespace BlazorApp.Pages.Clients
{
    public partial class CreateUserrazor
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        public UserDto UserData { get; set; } = new();

        protected async Task SaveUser()
        {
            UserData.CreatedAt = DateTime.UtcNow;
            var response = await Http.PostAsJsonAsync("https://localhost:7214/User", UserData);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Users");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json Response: \n" + strResponse);
            }
        }

    }
}
