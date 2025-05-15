using System.Net.Http.Json;
using ClassLibrary.Dto;


namespace BlazorApp.Services
{

    // unsure if anything works

    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _http;

        public TransactionService(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<TransactionDto>> GetTransactionsAsync()
        {
            return await _http.GetFromJsonAsync<List<TransactionDto>>("Transaction/All");
        }

        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<TransactionDto>($"Transaction/{id}");
        }
        public async Task<List<TransactionDto>> GetLastFiveAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/LastFive");
        }

        public async Task<List<TransactionDto>> GetAllByAccountIdAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"Transaction/AllByAccountId");
        }


        public async Task<decimal> GetExpensesAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Expenses");
        }

        public async Task<decimal> GetIncomesAsync(int accountId)
        {
            return await _http.GetFromJsonAsync<decimal>($"Transaction/Incomes");
        }


        public async Task AddTransactionAsync(CreateTransactionDto dto)
        {
            var response = await _http.PostAsJsonAsync("Transaction", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionDto dto)
        {
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