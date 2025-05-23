﻿using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.Account;
using BlazorApp.Services;


namespace BlazorApp.Pages.Account
{
    public partial class CreateAccount
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;

        public CreateAccountDto AccountData { get; set; } = new();

        protected async Task SaveAccount()
        {
            var response = await AccountService.AddAccountAsync(AccountData);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Accounts");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json Response: \n" + strResponse);
            }
        }

    }
}
