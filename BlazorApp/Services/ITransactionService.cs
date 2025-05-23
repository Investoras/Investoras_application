using ClassLibrary.Dto;
using ClassLibrary.Dto.Transaction;


namespace BlazorApp.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetTransactionsAsync();
        Task<TransactionDto> GetByIdAsync(int id);
        Task<List<TransactionDto>> GetLastFiveAsync(int accountId);
        Task<List<TransactionDto>> GetAllByAccountIdAsync(int accountId);

        Task<decimal> GetExpensesAsync(int accountId);
        Task<decimal> GetIncomesAsync(int accountId);

        Task AddTransactionAsync(CreateTransactionDto dto);
        Task UpdateTransactionAsync(int id, UpdateTransactionDto dto);
        Task DeleteTransactionAsync(int id);


    }
}
