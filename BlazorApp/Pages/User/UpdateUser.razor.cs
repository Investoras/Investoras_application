using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp.Models.User;
using ClassLibrary.Dto.User;
using BlazorApp.Mappings;
using BlazorApp.Services;

namespace BlazorApp.Pages.User
{
    public partial class UpdateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        [Parameter] public int Id { get; set; }

        public UserModel User { get; set; }
        public UpdateUserModel UserData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public bool IsSubmitting { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Id = AuthService.GetUserId();
            }
            catch
            {
                NavigationManager.NavigateTo("/login");
                return;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var userDto = await Http.GetFromJsonAsync<UserDto>($"User/{Id}");
                if (Id != userDto.UserId)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                } 
                if (userDto != null)
                {
                    User = userDto.ToModel();
                    UserData = userDto.ToUpdateModel(); 
                }
            }
            catch
            {
                ServerErrors.Add("Ошибка параметров: проверьте правильность введённых данных.");
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
                    NavigationManager.NavigateTo("/Main");
                }
                else
                {
                    ServerErrors.Add("Ошибка операции.");
                }
            }
            catch
            {
                ServerErrors.Add("Ошибка.");
            }
            finally
            {
                IsSubmitting = false;
            }
        }

    }
}
