﻿using System.Net.Http.Json;
using ClassLibrary.Dto.Transaction;


namespace BlazorApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public TransactionService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }


        public async Task<List<TransactionDto>> GetTransactionsAsync()
        {
            return await _http.GetFromJsonAsync<List<TransactionDto>>("https://localhost:7214/Transaction/All");
        }

        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<TransactionDto>($"https://localhost:7214/Transaction/{id}");
        }
        public async Task<List<TransactionDto>> GetLastFiveAsync(int accountId)
        {
<<<<<<< HEAD
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"https://localhost:7214/Transaction/LastFive?id={accountId}");
=======
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"https://localhost:7214/Transaction/LastFive");
>>>>>>> _piechart
        }
        // something like
        //public async Task<List<TransactionDto>> GetLastFiveAsync()
        //{
        //    if (_authService.UserId == null)
        //        throw new InvalidOperationException("User is not authenticated.");

        //    return await _http.GetFromJsonAsync<List<TransactionDto>>(
        //        $"https://localhost:7214/Transaction/LastFive?id={_authService.UserId}");
        //}

        public async Task<List<TransactionDto>> GetAllByAccountIdAsync(int accountId)
        {
<<<<<<< HEAD
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"https://localhost:7214/Transaction/AllByAccountId?id={accountId}");
=======
            return await _http.GetFromJsonAsync<List<TransactionDto>>($"https://localhost:7214/Transaction/AllByAccountId");
>>>>>>> _piechart
        }


        public async Task<decimal> GetExpensesAsync(int accountId)
        {
<<<<<<< HEAD
            return await _http.GetFromJsonAsync<decimal>($"https://localhost:7214/Transaction/Expenses?id={accountId}");
=======
            return await _http.GetFromJsonAsync<decimal>($"https://localhost:7214/Transaction/Expenses");
>>>>>>> _piechart
        }

        public async Task<decimal> GetIncomesAsync(int accountId)
        {
<<<<<<< HEAD
            return await _http.GetFromJsonAsync<decimal>($"https://localhost:7214/Transaction/Incomes?id={accountId}");
=======
            return await _http.GetFromJsonAsync<decimal>($"https://localhost:7214/Transaction/Incomes");
>>>>>>> _piechart
        }


        public async Task AddTransactionAsync(CreateTransactionDto dto)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7214/Transaction", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionDto dto)
        {
            var response = await _http.PutAsJsonAsync($"https://localhost:7214/Transaction/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7214/Transaction/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}