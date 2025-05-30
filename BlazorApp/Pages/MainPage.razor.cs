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

        protected List<TransactionModel> transactions = new();
        protected List<CategoryModel> categories = new();
        protected List<AccountModel> accounts = new();
        protected CreateTransactionModel newTransaction = new();
        protected bool showAddModal = false;
        protected bool isIncomeSelected = false;
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
            UpdatePieChart();
            await _pieChart.Update();
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

        private async void RadioInputChanged(bool value)
        {
            isIncomeSelected = value;
            UpdatePieChart();
            await _pieChart.Update();
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
            var transactionsByCategories = transactions.
                Where(t => isIncomeSelected == t.IsIncome && t.Date.Month == DateTime.Now.Month).
                GroupBy(t => t.CategoryId).
                Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                }).
                ToList();

            List<string> colorHexCodes = new List<string>();
            int categoriesCount = categories.Count;
            const double golden_ratio_conjugate = 0.618033988749895;
            Random random = new();
            double h = random.NextDouble();

            _pieConfig.Data.Labels.Clear();
            _pieConfig.Data.Datasets.Clear();

            var filteredCatrgories = categories.Where(c => transactionsByCategories.Any(tc => tc.Category == c.CategoryId)).ToList();

            foreach (var category in filteredCatrgories)
            {
                _pieConfig.Data.Labels.Add(category.Name);
                h += golden_ratio_conjugate;
                h %= 1.0;
                colorHexCodes.Add(HsvToRgbString(h * 360, 0.7, 0.99));
            }

            PieDataset<decimal> dataset = new PieDataset<decimal>(transactionsByCategories.
                Select(g => g.Total).ToList())
            {
                BackgroundColor = colorHexCodes.ToArray()
            };

            _pieConfig.Options.Title.Text = $"{(isIncomeSelected ? "Доходы" : "Расходы")} за {new CultureInfo("ru-RU").DateTimeFormat.GetMonthName(DateTime.Now.Month)}";

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
                        Text = $"{(isIncomeSelected ? "Доходы" : "Расходы")} за {new CultureInfo("ru-RU").DateTimeFormat.GetMonthName(DateTime.Now.Month)}",
                        FontSize = 18
                    },
                }
            };

            UpdatePieChart();
        }
    }
}