using ClassLibrary.Dto.Account;


namespace BlazorApp.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> AddAccountAsync(CreateAccountDto dto);
        Task<HttpResponseMessage> DeleteAccountAsync(int id);
        Task<List<AccountDto>> GetAllAccountsAsync();
        Task<decimal> GetBalanceAsync(int id);
        Task<AccountDto?> GetByIdAsync(int id);
        Task<HttpResponseMessage> UpdateAccountAsync(int id, UpdateAccountDto dto);
    }
}