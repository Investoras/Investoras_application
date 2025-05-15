using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Clients
{
    public partial class DeleteUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }

        protected async Task Delete_User()
        {
            var response = await Http.DeleteAsync("https://localhost:7214/User/" + Id);
            Navigation.NavigateTo("/Users");
        }
    }
}
