using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp.Models.User;
using ClassLibrary.Dto.User;
using BlazorApp.Mappings;

namespace BlazorApp.Pages.User
{
    public partial class UpdateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        [Parameter] public int Id { get; set; }

        public UserModel User { get; set; }
        public UpdateUserModel UserData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public bool IsSubmitting { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var userDto = await Http.GetFromJsonAsync<UserDto>($"User/{Id}");
                if (userDto != null)
                {
                    User = userDto.ToModel();
                    UserData = userDto.ToUpdateModel(); 
                }
            }
            catch
            {
                ServerErrors.Add("Update error.");
            }
        }

        protected async Task SaveUser()
        {
            ServerErrors.Clear();
            IsSubmitting = true;

            try
            {
                var response = await Http.PutAsJsonAsync($"User/{Id}", UserData.ToDto()); 
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
