using ClassLibrary.Dto.Account;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet("All")]
    public async Task<ActionResult> GetAllAccounts(CancellationToken cancellationToken)
    {
        var allAccounts = await _accountService.GetAllAccounts(cancellationToken);
        return Ok(allAccounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(int id, CancellationToken cancellationToken)
    {
        var account = await _accountService.GetAccountById(id, cancellationToken);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpGet("Balance/{id:int}")]
    public async Task<IActionResult> GetBalance(int id, CancellationToken cancellationToken)
    {
        var balance = await _accountService.GetTotalBalanceById(id, cancellationToken);
        return Ok(balance);
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount(CreateAccountDto accountDto, CancellationToken cancellationToken)
    {
        var account = await _accountService.CreateAccount(accountDto, cancellationToken);
        return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDto accountDto, CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.UpdateAccount(id, accountDto, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAccount(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.DeleteAccount(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}