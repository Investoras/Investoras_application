using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.Category;
using BlazorApp.Services;
using BlazorApp.Models.Transaction;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using System.Globalization;
using ChartJs.Blazor;
using ClassLibrary.Dto.Account;
using BlazorApp.Models.Account;
using BlazorApp.Models.Category;


namespace BlazorApp.Pages
{
    public partial class MainPage : ComponentBase
    {
        [Inject] private ITransactionService TransactionService { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] private IAccountService AccountService { get; set; } = default!;

        protected List<TransactionModel> transactions = new();
        protected List<CategoryModel> categories = new();
        protected List<AccountModel> accounts = new();
        protected CreateTransactionModel newTransaction = new();
        protected bool showAddModal = false;
        protected Chart? _pieChart;
        protected PieConfig? _pieConfig;

        //there are controllers for this
        protected decimal TotalIncome => transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
        protected decimal TotalExpense => transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
        protected decimal Balance => TotalIncome - TotalExpense;
        protected List<TransactionModel> RecentTransactions =>
            transactions.OrderByDescending(t => t.Date).Take(5).ToList();

        protected void ShowAddTransactionModal() => (showAddModal, newTransaction) = (true, new CreateTransactionModel { });
        protected void HideAddTransactionModal() => showAddModal = false;

        protected async Task AddTransaction()
        {
            await TransactionService.AddTransactionAsync(newTransaction);
            await LoadTransactions();
            showAddModal = false;
            newTransaction = new CreateTransactionModel();
            SetupPieChart();
        }

        protected override async Task OnInitializedAsync()
        {
            accounts = await AccountService.GetAllAccountsAsync();
            categories = await CategoryService.GetAllAsync();
            await LoadTransactions();
            SetupPieChart();
        }

        private async Task LoadTransactions()
        {
            transactions = await TransactionService.GetAllTransactionsAsync();
        }


        private void SetupPieChart()
        {
            _pieConfig = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        // не круто, потому что месяц берется по времени системы, при этом транзакции
                        // не отсортированы по текущему месяцу, но работает (заглушка)
                        Text = new CultureInfo("ru-RU").DateTimeFormat.GetMonthName(DateTime.Now.Month),
                        FontSize = 18
                    },
                }
            };

            var transactionsByCategories = transactions.
                Where(t => !t.IsIncome).
                GroupBy(t => t.CategoryId).
                Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                }).
                ToList();

            var colors = transactionsByCategories.Select(_ => ColorUtil.RandomColorString()).ToArray();

            _pieConfig.Data.Labels.Clear();
            foreach (var categoryLabel in categories.ToList())
                _pieConfig.Data.Labels.Add(categoryLabel.Name);

            PieDataset<decimal> dataset = new PieDataset<decimal>(transactionsByCategories.
                Select(g => g.Total).ToList())
            {
                BackgroundColor = colors
            };

            _pieConfig.Data.Datasets.Add(dataset);
        }
    }
}
