using BlazorApp.Models.Transaction;

namespace BlazorApp.Services
{
    public interface ITransactionService
    {
        Task<TransactionModel> AddTransactionAsync(CreateTransactionModel model);
        Task DeleteTransactionAsync(int id);
        Task<List<TransactionModel>> GetAllTransactionsAsync();
        Task<List<TransactionModel>> GetAllTransactionsByAccountIdAsync(int accountId);
        Task<List<TransactionModel>> GetLastFiveTransactionsByAccountIdAsync(int accountId);
        Task<decimal> GetSumOfExpensesByAccountIdAsync(int accountId);
        Task<decimal> GetSumOfIncomeByAccountIdAsync(int accountId);
        Task<TransactionModel?> GetTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(int id, UpdateTransactionModel model);
    }
}