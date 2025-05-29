using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.Transaction;
using Investoras_Backend.Data.Entities;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ClassLibrary.Dto.Account;
using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactions(CancellationToken cancellationToken);
    Task<TransactionDto> GetTransactionById(int id, CancellationToken cancellationToken);
    Task<TransactionDto> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task DeleteTransaction(int id, CancellationToken cancellationToken);
    Task<decimal> GetSumOfExpensesByAccountId(int id, CancellationToken cancellationToken);
    Task<decimal> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionDto>> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionDto>> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken);
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
    public async Task<TransactionDto> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transactionModel = new TransactionModel
        {
             AccountId = transactionDto.AccountId,
             Amount = transactionDto.Amount,
             CategoryId = transactionDto.CategoryId,
             Description = transactionDto.Description,
             Date = transactionDto.Date
             
        };

        var validationContext = new ValidationContext(transactionModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            transactionModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }

        var entity = _mapper.Map<Transaction>(transactionModel);
        _context.Transactions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<TransactionDto>(entity);
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
        if (expenses == null) throw new NotFoundException("Транзакции не найдены");
        foreach (var expense in expenses)
        {
            sum += expense.Amount;
        }
        return sum;
    }
    public async Task<decimal> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken)
    {
        decimal sum = 0;
        var incomes = await _context.Transactions.Where(u => u.AccountId == id && u.Category.IsIncome == true).ToListAsync(cancellationToken);
        if (incomes == null) throw new NotFoundException("Транзакции не найдены");
        foreach (var income in incomes)
        {
            sum += income.Amount;
        }
        return sum;
    }

    //public async Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken)
    //{
    //    var allTransactions = await _context.Transactions.ToListAsync(cancellationToken);
    //    return _mapper.Map<IEnumerable<TransactionModel>>(allTransactions);
    //}
    public async Task<IEnumerable<TransactionDto>> GetAllTransactions(CancellationToken cancellationToken)
    {
        var allTransactions = await _context.Transactions
            .Include(t => t.Category)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TransactionDto>>(allTransactions);
    }
    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken)
    {
        var allTransactions = await _context.Transactions.Where(t => t.AccountId == id).ToListAsync(cancellationToken);
        if (allTransactions == null) throw new NotFoundException("Транзакции не найдены");
        return _mapper.Map<IEnumerable<TransactionDto>>(allTransactions);
    }
    public async Task<TransactionDto> GetTransactionById(int id, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id, cancellationToken);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transactionModel = new TransactionModel
        {
            AccountId = transactionDto.AccountId,
            Amount = transactionDto.Amount,
            CategoryId = transactionDto.CategoryId,
            Description = transactionDto.Description,
            Date = transactionDto.Date
        };

        var validationContext = new ValidationContext(transactionModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            transactionModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) throw new NotFoundException("Транзакция не найдена");
        transactionModel.TransactionId = transaction.TransactionId;

        _mapper.Map(transactionModel, transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TransactionDto>> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken)
    {
            var lastFiveTransactions = await _context.Transactions
            .Where(t => t.AccountId == id)
            .OrderByDescending(t => t.Date)
            .Take(5)
            .ToListAsync(cancellationToken);
        if (lastFiveTransactions == null) throw new NotFoundException("Транзакции не найдены");
            return _mapper.Map<IEnumerable<TransactionDto>>(lastFiveTransactions);

    }
}
