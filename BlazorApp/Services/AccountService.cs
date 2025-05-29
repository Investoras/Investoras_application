using BlazorApp.Models.Account;
using ClassLibrary.Dto.Account;
using System.Net.Http.Json;
using BlazorApp.Mappings;

namespace BlazorApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _http;

        public AccountService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AccountModel>> GetAllAccountsAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<AccountDto>>("Account/All");
            return dtos?.ToModel() ?? new List<AccountModel>();
        }

        public async Task<AccountModel?> GetByIdAsync(int id)
        {
            var dto = await _http.GetFromJsonAsync<AccountDto>($"Account/{id}");
            return dto?.ToModel();
        }

        public async Task<decimal> GetBalanceAsync(int id)
        {
            return await _http.GetFromJsonAsync<decimal>($"Account/Balance/{id}");
        }

        public async Task<HttpResponseMessage> AddAccountAsync(CreateAccountModel model)
        {
            var dto = model.ToDto();
            return await _http.PostAsJsonAsync("Account", dto);
        }

        public async Task<HttpResponseMessage> UpdateAccountAsync(int id, UpdateAccountModel model)
        {
            var dto = model.ToDto();
            return await _http.PutAsJsonAsync($"Account/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAccountAsync(int id)
        {
            return await _http.DeleteAsync($"Account/{id}");
        }
    }
}
