using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Account
{
    public partial class DeleteAccount
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }

        protected async Task Delete_Account()
        {
            var response = await AccountService.DeleteAccountAsync(Id);
            Navigation.NavigateTo("/Accounts");
        }
    }
}