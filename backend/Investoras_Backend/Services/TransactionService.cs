using AutoMapper;
using Investoras_Backend.Data;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken);
    Task<TransactionModel> GetTransactionById(int id, CancellationToken cancellationToken);
    Task<TransactionModel> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task DeleteTransaction(int id, CancellationToken cancellationToken);
    Task<decimal> GetSumOfExpensesByAccountId(int id, CancellationToken cancellationToken);
    Task<decimal> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionModel>> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionModel>> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken);
}
public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public TransactionService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<TransactionModel> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transactionModel = TransactionModel.Create(transactionDto.Amount, transactionDto.Description,transactionDto.AccountId, transactionDto.CategoryId);
        var entity = _mapper.Map<Transaction>(transactionModel);
        _context.Transactions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<TransactionModel>(entity);
    }

    public async Task DeleteTransaction(int id, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) throw new NotFoundException("Транзакция не найдена");
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<decimal> GetSumOfExpensesByAccountId(int id, CancellationToken cancellationToken)
    {
        decimal sum = 0;
        var expenses = await _context.Transactions.Where(u => u.AccountId==id && u.Category.IsIncome == false).ToListAsync(cancellationToken);
        foreach (var expense in expenses)
        {
            sum += expense.Amount;
        }
        return sum;
    }
    public async Task<decimal> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken)
    {
        decimal sum = 0;
        var expenses = await _context.Transactions.Where(u => u.AccountId == id && u.Category.IsIncome == true).ToListAsync(cancellationToken);
        foreach (var expense in expenses)
        {
            sum += expense.Amount;
        }
        return sum;
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken)
    {
        var allTransactions = await _context.Transactions.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TransactionModel>>(allTransactions);
    }
    public async Task<IEnumerable<TransactionModel>> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken)
    {
        var allTransactions = await _context.Transactions.Where(t => t.AccountId == id).ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TransactionModel>>(allTransactions);
    }
    public async Task<TransactionModel> GetTransactionById(int id, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id, cancellationToken);
        return _mapper.Map<TransactionModel>(transaction);
    }

    public async Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) throw new NotFoundException("Транзакция не найдена");

        _mapper.Map(transactionDto, transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TransactionModel>> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken)
    {
        var lastFiveTransactions = await _context.Transactions
        .Where(t => t.AccountId == id)
        .OrderByDescending(t => t.Date) 
        .Take(5)
        .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TransactionModel>>(lastFiveTransactions);
    }
}
