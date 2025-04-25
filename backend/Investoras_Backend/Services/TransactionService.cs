using AutoMapper;
using Investoras_Backend.Data;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactions(CancellationToken cancellationToken);
    Task<TransactionDto> GetTransactionById(int id, CancellationToken cancellationToken);
    Task<TransactionDto> CreateTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken);
    Task DeleteTransaction(int id, CancellationToken cancellationToken);
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
        var transaction = _mapper.Map<Transaction>(transactionDto);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task DeleteTransaction(int id, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) throw new NotFoundException("Product not found");
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactions(CancellationToken cancellationToken)
    {
        var allTransactions = await _context.Transactions.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TransactionDto>>(allTransactions);
    }

    public async Task<TransactionDto> GetTransactionById(int id, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) throw new NotFoundException("Product not found");

        _mapper.Map(transactionDto, transaction);
        await _context.SaveChangesAsync();
    }
}
