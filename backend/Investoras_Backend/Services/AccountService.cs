using AutoMapper;
using Investoras_Backend.Data;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountModel>> GetAllAccounts(CancellationToken cancellationToken);
    Task<AccountModel> GetAccountById(int id, CancellationToken cancellationToken);
    Task<AccountModel> CreateAccount(CreateAccountDto accountDto, CancellationToken cancellationToken);
    Task UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken);
    Task DeleteAccount(int id, CancellationToken cancellationToken);
    Task<decimal> GetTotalBalanceById(int id, CancellationToken cancellationToken);
}
public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public AccountService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<AccountModel> CreateAccount(CreateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var accountModel = AccountModel.Create(accountDto.Name, accountDto.Balance, accountDto.UserId);
        var entity = _mapper.Map<Account>(accountModel);
        _context.Accounts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AccountModel>(entity);
    }

    public async Task DeleteAccount(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) throw new NotFoundException("Аккаунт не найден");
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AccountModel>> GetAllAccounts(CancellationToken cancellationToken)
    {
        var allAccounts = await _context.Accounts.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AccountModel>>(allAccounts);
    }

    public async Task<AccountModel> GetAccountById(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id, cancellationToken);
        return _mapper.Map<AccountModel>(account);
    }

    public async Task UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id, cancellationToken);
        if (account == null) throw new NotFoundException("Аккаунт не найден");

        _mapper.Map(accountDto, account);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalBalanceById(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id, cancellationToken); 
        var expenses = await _context.Transactions.Where(u => u.AccountId == id && u.Category.IsIncome == false).ToListAsync(cancellationToken);
        var income = await _context.Transactions.Where(u => u.AccountId == id && u.Category.IsIncome == true).ToListAsync(cancellationToken);
        foreach (var exp in expenses) {
            account.Balance -= exp.Amount;
        }
        foreach (var inc in income)
        {
            account.Balance += inc.Amount;
        }
        if (account == null) throw new NotFoundException("Аккаунт не найден");
        return account.Balance;
    }
}
