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
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private int? UserId { get; set; }
        protected List<string> ServerErrors { get; set; } = new();
        protected CreateAccountModel AccountData { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserId = AuthService.GetUserId();
            }
            catch
            {
                NavigationManager.NavigateTo("/login");
                return;
            }
        }

        protected async Task SaveAccount()
        {
            try
            {
                AccountData.UserId = (int)UserId;
                var response = await AccountService.AddAccountAsync(AccountData);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/Accounts");
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
        }

    }
}
