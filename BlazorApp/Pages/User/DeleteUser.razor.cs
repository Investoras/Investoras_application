using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.User
{
    public partial class DeleteUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private int? UserId { get; set; }
        // FOR TESTING - TO ACCESS USER MANAGEMENT FUNCTION
        private readonly bool Superuser = false;

        [Parameter]
        public int Id { set; get; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (!Superuser)
                {
                    Navigation.NavigateTo("/");
                    return;
                }
                UserId = AuthService.GetUserId();
            }
            catch
            {
                Navigation.NavigateTo("/login");
                return;
            }
        }

        protected async Task Delete_User()
        {
            var response = await Http.DeleteAsync("User/" + Id);
            Navigation.NavigateTo("/Users");
        }
    }
}
