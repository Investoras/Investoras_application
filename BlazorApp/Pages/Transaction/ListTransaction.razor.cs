using ClassLibrary.Dto.Transaction;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using BlazorApp.Models.Transaction;
using BlazorApp.Services;
using BlazorApp.Models.Account;

namespace BlazorApp.Pages.Transaction;


public partial class ListTransaction
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private ITransactionService TransactionService { get; set; } = default!;
    [Inject] private ICategoryService CategoryService { get; set; } = default!;
    [Inject] private IAccountService AccountService { get; set; } = default!;
    [Inject] private IAuthService AuthService { get; set; } = default!;

    protected List<AccountModel> accounts = new();

    private List<TransactionDto>? transactions;

    protected override async Task OnInitializedAsync()
    {
        accounts = await AccountService.GetAllAccountsAsync();
        transactions = await Http.GetFromJsonAsync<List<TransactionDto>>("Transaction/All");
        transactions = transactions.Join(accounts,
                                            t => t.AccountId,
                                            a => a.AccountId,
                                            (t, a) => new { Transaction = t, Account = a })
                                        .Where(ta => ta.Account.UserId == AuthService.UserId)
                                        .Select(ta => ta.Transaction)
                                        .ToList();
    }
}
