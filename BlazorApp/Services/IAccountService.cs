using BlazorApp.Models.Account;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> AddAccountAsync(CreateAccountModel model);
        Task<HttpResponseMessage> DeleteAccountAsync(int id);
        Task<List<AccountModel>> GetAllAccountsAsync();
        Task<decimal> GetBalanceAsync(int id);
        Task<AccountModel?> GetByIdAsync(int id);
        Task<HttpResponseMessage> UpdateAccountAsync(int id, UpdateAccountModel model);
    }
}