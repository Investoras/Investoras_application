using ClassLibrary.Dto.Account;
using Microsoft.AspNetCore.Components;
using BlazorApp.Services;
using BlazorApp.Models.Account;
using BlazorApp.Models.Transaction;

namespace BlazorApp.Pages.Account;


public partial class Index
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAccountService AccountService { get; set; } = default!;
    [Inject] private IAuthService AuthService { get; set; } = default!;

    [Parameter] public int Id { set; get; }
    private int? UserId { get; set; }

    private List<AccountModel>? accounts;

    protected bool showDeleteModal = false;

    protected void ShowDeleteModal(int accountId) { showDeleteModal = true; Id = accountId; }
    protected void HideDeleteModal() => showDeleteModal = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserId = AuthService.GetUserId();
            var allAccounts = await AccountService.GetAllAccountsAsync();
            accounts = allAccounts.Where(a => a.UserId.Equals(UserId)).ToList();
        }
        catch
        {
            NavigationManager.NavigateTo("/login");
            return;
        }
    }

    protected async Task Delete_Account()
    {
        var response = await AccountService.DeleteAccountAsync(Id);
        NavigationManager.NavigateTo("/Accounts");
        showDeleteModal = false;
        await OnInitializedAsync();
    }
}