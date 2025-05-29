using BlazorApp.Models;

namespace BlazorApp.Services
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(TransactionModel model);
        Task DeleteTransactionAsync(int id);
        Task<List<TransactionModel>> GetAllByAccountIdAsync(int accountId);
        Task<TransactionModel> GetByIdAsync(int id);
        Task<decimal> GetExpensesAsync(int accountId);
        Task<decimal> GetIncomesAsync(int accountId);
        Task<List<TransactionModel>> GetLastFiveAsync(int accountId);
        Task<List<TransactionModel>> GetTransactionsAsync();
        Task<HttpResponseMessage> UpdateTransactionAsync(int id, TransactionModel model);
    }
}