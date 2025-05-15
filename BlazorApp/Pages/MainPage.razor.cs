using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto;
using BlazorApp.Services;


namespace BlazorApp.Pages
{
    public partial class MainPage : ComponentBase
    {
        [Inject] private ITransactionService TransactionService { get; set; } = default!;

        protected List<TransactionDto> transactions = new();
        protected CreateTransactionDto newTransaction = new();
        protected bool showAddModal = false;

        protected decimal TotalIncome => transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
        protected decimal TotalExpense => transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
        protected decimal Balance => TotalIncome - TotalExpense;
        protected List<TransactionDto> RecentTransactions => transactions
            .OrderByDescending(t => t.Date)
            .Take(5)
            .ToList();

        protected void ShowAddTransactionModal() => (showAddModal, newTransaction) = (true, new CreateTransactionDto { Date = DateTime.Now });
        protected void HideAddTransactionModal() => showAddModal = false;

        protected async Task AddTransaction()
        {
            await TransactionService.AddTransactionAsync(newTransaction);
            transactions = await TransactionService.GetTransactionsAsync();
            showAddModal = false;
        }

        protected override async Task OnInitializedAsync()
        {
            transactions = await TransactionService.GetTransactionsAsync();
        }
    }
}