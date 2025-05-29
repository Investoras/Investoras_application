using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.Account;
using BlazorApp.Services;
using BlazorApp.Models.Account;
using ClassLibrary.Dto.Category;


namespace BlazorApp.Pages.Account
{
    public partial class CreateAccount
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;

        public List<string> ServerErrors { get; set; } = new();
        public CreateAccountModel AccountData { get; set; } = new();

        protected async Task SaveAccount()
        {
            try
            {
                var response = await AccountService.AddAccountAsync(AccountData);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/Accounts");
                }
                else
                {
                    ServerErrors.Add("Account error.");
                }
            }
            catch
            {
                ServerErrors.Add("Account error.");
            }
        }

    }
}
