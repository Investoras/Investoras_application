using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using ClassLibrary.Dto.User;


namespace BlazorApp.Pages.Clients
{
    public partial class UpdateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }
        public UserDto user = null;
        public UpdateUserDto userData { set; get; } = new();
        public JsonNode Errors { set; get; } = new JsonObject();

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                user = await Http.GetFromJsonAsync<UserDto>("https://localhost:7214/User/" + Id);
                userData.Username = user?.Username;
                userData.Email = user.Email;
                userData.Password = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        protected async Task SaveUser()
        {
            var response = await Http.PutAsJsonAsync("https://localhost:7214/User/" + Id, userData);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Users");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    var jsonResponse = JsonNode.Parse(strResponse);
                    Errors = jsonResponse?["errors"] ?? new JsonObject();

                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}
