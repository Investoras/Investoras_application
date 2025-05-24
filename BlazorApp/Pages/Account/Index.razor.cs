using ClassLibrary.Dto.Account;
using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Account;


public partial class Index
{
    [Inject] private IAccountService AccountService { get; set; } = default!;

    private List<AccountDto>? accounts;

    protected override async Task OnInitializedAsync()
    {
        accounts = await AccountService.GetAllAccountsAsync();
    }
}