using ClassLibrary.Dto.Account;
using System.Net.Http.Json;


namespace BlazorApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _http;

        public AccountService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AccountDto>> GetAllAccountsAsync() 
            => await _http.GetFromJsonAsync<List<AccountDto>>("https://localhost:7214/Account/All");

        public async Task<AccountDto?> GetByIdAsync(int id) 
            => await _http.GetFromJsonAsync<AccountDto>($"https://localhost:7214/Account/{id}");

        public async Task<decimal> GetBalanceAsync(int id) 
            => await _http.GetFromJsonAsync<decimal>($"https://localhost:7214/Account/Balance?id={id}");

        public async Task<HttpResponseMessage> AddAccountAsync(CreateAccountDto dto)
            => await _http.PostAsJsonAsync("https://localhost:7214/Account", dto);

        public async Task<HttpResponseMessage> UpdateAccountAsync(int id, UpdateAccountDto dto)
            => await _http.PutAsJsonAsync($"https://localhost:7214/Account/{id}", dto);

        public async Task<HttpResponseMessage> DeleteAccountAsync(int id) 
            => await _http.DeleteAsync($"https://localhost:7214/Account/{id}");
    }
}
