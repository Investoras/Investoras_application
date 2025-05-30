using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using ClassLibrary.Dto.Account;
using BlazorApp.Services;
using BlazorApp.Models.Account;
using BlazorApp.Models.Category;
using ClassLibrary.Dto.Category;


namespace BlazorApp.Pages.Account
{
    public partial class UpdateAccount
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;
        [Inject] private IAuthService AuthService { get; set; } = default!;

        private int? LoggedUserId { get; set; }

        [Parameter]
        public int Id { set; get; }
        public AccountModel Account = null;
        public UpdateAccountModel AccountData { set; get; } = new();
        public List<string> ServerErrors { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                LoggedUserId = AuthService.GetUserId();
            }
            catch
            {
                NavigationManager.NavigateTo("/login");
                return;
            }
        }

        // datetimo is set to default on update for some reason
        protected override async Task OnParametersSetAsync()
        {
            var account = await AccountService.GetByIdAsync(Id);
            if (account != null)
            {
                AccountData = new UpdateAccountModel
                {
                    Name = account?.Name,
                    Balance = account.Balance,
                    UserId = (int)LoggedUserId,
                    CreatedAt = account.CreatedAt
                };
            }
            else
            {
                ServerErrors.Add("Ошибка счета.");
            }
        }

        protected async Task SaveAccount()
        {
            ServerErrors.Clear();
            try
            {
                var response = await AccountService.UpdateAccountAsync(Id, AccountData);
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
