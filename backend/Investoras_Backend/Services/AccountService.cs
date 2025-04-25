using AutoMapper;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data;
using SendGrid.Helpers.Errors.Model;
using Microsoft.EntityFrameworkCore;

namespace Investoras_Backend.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken);
    Task<AccountDto> GetAccountById(int id, CancellationToken cancellationToken);
    Task<AccountDto> CreateAccount(CreateAccountDto accountDto, CancellationToken cancellationToken);
    Task UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken);
    Task DeleteAccount(int id, CancellationToken cancellationToken);
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
        var account = _mapper.Map<Account>(accountDto);
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return _mapper.Map<AccountDto>(account);
    }

    public async Task DeleteAccount(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) throw new NotFoundException("Product not found");
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
        var account = await _context.Accounts.FindAsync(id);
        return _mapper.Map<AccountDto>(account);
    }

    public async Task UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) throw new NotFoundException("Product not found");

        _mapper.Map(accountDto, account);
        await _context.SaveChangesAsync();
    }
}
