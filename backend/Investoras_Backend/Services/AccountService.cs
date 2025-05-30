using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.Account;
using Investoras_Backend.Data.Entities;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken);
    Task<AccountDto> GetAccountById(int id, CancellationToken cancellationToken);
    Task<AccountDto> CreateAccount(CreateAccountDto accountDto, CancellationToken cancellationToken);
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
    public async Task<AccountDto> CreateAccount(CreateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var accountModel = new AccountModel
        {
            Balance = accountDto.Balance,
            Name = accountDto.Name,
            UserId = accountDto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        var validationContext = new ValidationContext(accountModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            accountModel,
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
        var entity = _mapper.Map<Account>(accountModel);
        _context.Accounts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AccountDto>(entity);
    }

    public async Task DeleteAccount(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) throw new NotFoundException("Аккаунт не найден");
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken)
    {
        var allAccounts = await _context.Accounts.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AccountDto>>(allAccounts);
    }

    public async Task<AccountDto> GetAccountById(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id, cancellationToken);
        return _mapper.Map<AccountDto>(account);
    }

    public async Task UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var accountModel = new AccountModel
        {
            Balance = accountDto.Balance,
            Name = accountDto.Name,
            UserId = accountDto.UserId,
            CreatedAt = accountDto.CreatedAt
        };

        var validationContext = new ValidationContext(accountModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            accountModel,
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

        var account = await _context.Accounts.FindAsync(id, cancellationToken);
        if (account == null) throw new NotFoundException("Аккаунт не найден");
        accountModel.AccountId = account.AccountId;

        _mapper.Map(accountModel, account);
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
