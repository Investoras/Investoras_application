using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using ClassLibrary.Dto.Account;
using BlazorApp.Services;


namespace BlazorApp.Pages.Account
{
    public partial class UpdateAccount
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }
        public AccountDto Account = null;
        public UpdateAccountDto AccountData { set; get; } = new();
        public JsonNode Errors { set; get; } = new JsonObject();

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Account = await AccountService.GetByIdAsync(Id);
                AccountData.Name = Account?.Name;
                AccountData.Balance = Account.Balance;
                AccountData.UserId = Account.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        protected async Task SaveAccount()
        {
            var response = await AccountService.UpdateAccountAsync(Id, AccountData);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Accounts");
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
