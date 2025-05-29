using System.Net.Http.Json;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.Category;
using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;

        public TransactionService(HttpClient http, IAuthService authService, ICategoryService categoryService)
        {
            _http = http;
            _authService = authService;
            _categoryService = categoryService;
        }

        public async Task<List<TransactionModel>> GetTransactionsAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>("Transaction/All")
                       ?? new List<TransactionDto>();

            var categories = await _categoryService.GetAllAsync();
            var categoryDict = categories.ToDictionary(c => c.CategoryId);

            return dtos.Select(dto =>
            {
                categoryDict.TryGetValue(dto.CategoryId, out var category);
                return dto.ToModel(category);
            }).ToList();
        }

        public async Task<TransactionModel> GetByIdAsync(int id)
        {
            var dto = await _http.GetFromJsonAsync<TransactionDto>($"Transaction/{id}");
            if (dto == null) throw new Exception("Transaction not found");

            var category = await _categoryService.GetByIdAsync(dto.CategoryId);
            return dto.ToModel(category);
        }

        public async Task<List<TransactionModel>> GetLastFiveAsync(int accountId)
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/LastFive/{accountId}")
                       ?? new List<TransactionDto>();

            var categories = await _categoryService.GetAllAsync();
            var categoryDict = categories.ToDictionary(c => c.CategoryId);

            return dtos.Select(dto =>
            {
                categoryDict.TryGetValue(dto.CategoryId, out var category);
                return dto.ToModel(category);
            }).ToList();
        }

        public async Task<List<TransactionModel>> GetAllByAccountIdAsync(int accountId)
        {
            var dtos = await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/AllByAccountId/{accountId}")
                       ?? new List<TransactionDto>();

            var categories = await _categoryService.GetAllAsync();
            var categoryDict = categories.ToDictionary(c => c.CategoryId);

            return dtos.Select(dto =>
            {
                categoryDict.TryGetValue(dto.CategoryId, out var category);
                return dto.ToModel(category);
            }).ToList();
        }

        public async Task<decimal> GetExpensesAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Expenses/{accountId}");
        }

        public async Task<decimal> GetIncomesAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Incomes/{accountId}");
        }

        public async Task AddTransactionAsync(TransactionModel model)
        {
            CreateTransactionDto dto = model.ToCreateDto();
            var response = await _http.PostAsJsonAsync("Transaction", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> UpdateTransactionAsync(int id, TransactionModel model)
        {
            UpdateTransactionDto dto = model.ToUpdateDto();
            return await _http.PutAsJsonAsync($"Transaction/{id}", dto);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var response = await _http.DeleteAsync($"Transaction/{id}");
            response.EnsureSuccessStatusCode();
        }


    }
}
