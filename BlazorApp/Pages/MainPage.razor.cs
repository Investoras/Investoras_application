using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.Category;
using BlazorApp.Services;
using BlazorApp.Models.Transaction;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.PieChart;
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
        [Inject] private IAuthService AuthService { get; set; } = default!;

        protected List<TransactionModel> transactions = new();
        protected List<CategoryModel> categories = new();
        protected List<CategoryModel> filteredCategories = new();
        protected List<AccountModel> accounts = new();
        protected CreateTransactionModel newTransaction = new();
        protected bool showAddModal = false;
        protected bool isIncomeSelected = false;
        protected string selectedTransactionType = "";
        protected Chart? _pieChart;
        protected PieConfig? _pieConfig;

        //there are controllers for this
        protected decimal TotalIncome => transactions.Join(accounts,
                                                        t => t.AccountId,
                                                        a => a.AccountId,
                                                        (t, a) => new { Transaction = t, Account = a })
                                                    .Where(ta => ta.Account.UserId == AuthService.UserId && ta.Transaction.IsIncome)
                                                    .Sum(ta => ta.Transaction.Amount);
        protected decimal TotalExpense => transactions.Join(accounts,
                                                        t => t.AccountId,
                                                        a => a.AccountId,
                                                        (t, a) => new { Transaction = t, Account = a })
                                                    .Where(ta => ta.Account.UserId == AuthService.UserId && !ta.Transaction.IsIncome)
                                                    .Sum(ta => ta.Transaction.Amount);
        protected decimal Balance => accounts.Where(a => a.UserId == AuthService.UserId).Select(a => a.Balance).Sum() + TotalIncome - TotalExpense;
        protected List<TransactionModel> RecentTransactions =>
            transactions.OrderByDescending(t => t.Date).Take(5).ToList();

        protected void ShowAddTransactionModal() => (showAddModal, newTransaction) = (true, new CreateTransactionModel { });
        protected void HideAddTransactionModal() => showAddModal = false;
        protected bool HasChartData => transactions.Any(t => t.IsIncome == isIncomeSelected && t.Date.Month == DateTime.Now.Month);
        protected string CurrentMonthName => new CultureInfo("ru-RU").DateTimeFormat.GetMonthName(DateTime.Now.Month);
        protected async Task AddTransaction()
        {
            newTransaction.Date = DateTime.UtcNow;
            await TransactionService.AddTransactionAsync(newTransaction);
            await LoadTransactions();
            showAddModal = false;
            newTransaction = new CreateTransactionModel();
            UpdatePieChart();
            if (_pieChart != null && HasChartData)
            {
                await _pieChart.Update();
            }
            selectedTransactionType = "";
        }

        protected override async Task OnInitializedAsync()
        {
            accounts = await AccountService.GetAllAccountsAsync();
            accounts = accounts.Where(a => a.UserId == AuthService.UserId).ToList();
            categories = await CategoryService.GetAllAsync();
            await LoadTransactions();
            SetupPieChart();
        }

        private async Task LoadTransactions()
        {
            transactions = await TransactionService.GetAllTransactionsAsync();
            transactions = transactions.Join(accounts,
                                            t => t.AccountId,
                                            a => a.AccountId,
                                            (t, a) => new { Transaction = t, Account = a })
                                        .Where(ta => ta.Account.UserId == AuthService.UserId)
                                        .Select(ta => ta.Transaction)
                                        .ToList();
        }

        private async void RadioInputChanged(bool value)
        {
            isIncomeSelected = value;

            UpdatePieChart();
            if (_pieChart != null && HasChartData)
            {
                await _pieChart.Update();
            }
        }

        private void OnTransactionTypeChanged(ChangeEventArgs e)
        {
            selectedTransactionType = e.Value?.ToString() ?? "";
            isIncomeSelected = selectedTransactionType == "income";
            filteredCategories = new List<CategoryModel>();
            filteredCategories = categories.Where(c => c.IsIncome == isIncomeSelected).ToList();
        }
        private string HsvToRgbString(double h, double s, double v)
        {
            double c = v * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            double m = v - c;

            double r, g, b;
            switch (h)
            {
                case < 60:
                    r = c; g = x; b = 0;
                    break;
                case < 120:
                    r = x; g = c; b = 0;
                    break;
                case < 180:
                    r = 0; g = c; b = x;
                    break;
                case < 240:
                    r = 0; g = x; b = c;
                    break;
                case < 300:
                    r = x; g = 0; b = c;
                    break;
                default:
                    r = c; g = 0; b = x;
                    break;
            }

            byte R = (byte)((r + m) * 255);
            byte G = (byte)((g + m) * 255);
            byte B = (byte)((b + m) * 255);

            return $"#{R:X2}{G:X2}{B:X2}";
        }

        private void UpdatePieChart()
        {
            if (_pieConfig == null)
                return;

            var transactionsByCategories = transactions
                .Where(t => isIncomeSelected == t.IsIncome && t.Date.Month == DateTime.Now.Month)
                .GroupBy(t => t.CategoryId)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                })
                .ToList();

            _pieConfig.Data.Labels.Clear();
            _pieConfig.Data.Datasets.Clear();
            _pieConfig.Options.Title.Text = $"{(isIncomeSelected ? "Доходы" : "Расходы")} за {CurrentMonthName}";

            if (!transactionsByCategories.Any())
                return;

            List<string> colorHexCodes = new List<string>();
            const double golden_ratio_conjugate = 0.618033988749895;
            Random random = new();
            double h = random.NextDouble();

            filteredCategories = categories
                .Where(c => transactionsByCategories.Any(tc => tc.Category == c.CategoryId))
                .ToList();

            foreach (var category in filteredCategories)
            {
                _pieConfig.Data.Labels.Add(category.Name);
                h += golden_ratio_conjugate;
                h %= 1.0;
                colorHexCodes.Add(HsvToRgbString(h * 360, 0.7, 0.99));
            }

            var dataset = new PieDataset<decimal>(transactionsByCategories.Select(g => g.Total).ToList())
            {
                BackgroundColor = colorHexCodes.ToArray()
            };

            _pieConfig.Data.Datasets.Add(dataset);
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
                        Text = $"{(isIncomeSelected ? "Доходы" : "Расходы")} за {CurrentMonthName}",
                        FontSize = 18
                    },
                }
            };

            UpdatePieChart();
        }
    }
}