using System.Net.Http.Json;
using BlazorApp.Models.Transaction;
using ClassLibrary.Dto.Transaction;
using BlazorApp.Mappings;

namespace BlazorApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _http;
        private readonly ICategoryService _categoryService;

        public TransactionService(HttpClient http, ICategoryService categoryService)
        {
            _http = http;
            _categoryService = categoryService;
        }

        public async Task<List<TransactionModel>> GetAllTransactionsAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>("Transaction/All") ?? new();
            return await dtos.ToModelAsync(_categoryService);
        }

        public async Task<TransactionModel?> GetTransactionByIdAsync(int id)
        {
            var dto = await _http.GetFromJsonAsync<TransactionDto>($"Transaction/{id}");
            return dto == null ? null : await dto.ToModelAsync(_categoryService);
        }

        public async Task<decimal> GetSumOfExpensesByAccountIdAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Expenses/{accountId}");
        }

        public async Task<decimal> GetSumOfIncomeByAccountIdAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Incomes/{accountId}");
        }

        public async Task<List<TransactionModel>> GetLastFiveTransactionsByAccountIdAsync(int accountId)
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/LastFive/{accountId}") ?? new();
            return await dtos.ToModelAsync(_categoryService);
        }

        public async Task<List<TransactionModel>> GetAllTransactionsByAccountIdAsync(int accountId)
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/AllByAccountId/{accountId}") ?? new();
            return await dtos.ToModelAsync(_categoryService);
        }

        public async Task<TransactionModel> AddTransactionAsync(CreateTransactionModel model)
        {
            var dto = model.ToDto();
            var response = await _http.PostAsJsonAsync("Transaction", dto);
            response.EnsureSuccessStatusCode();

            var createdDto = await response.Content.ReadFromJsonAsync<TransactionDto>();
            return await createdDto!.ToModelAsync(_categoryService);
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionModel model)
        {
            var dto = model.ToDto();
            var response = await _http.PutAsJsonAsync($"Transaction/{id}", dto);
            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteTransactionAsync(int id)
        {
            var response = await _http.DeleteAsync($"Transaction/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
