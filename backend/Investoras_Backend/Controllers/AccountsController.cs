using Investoras_Backend.Data;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Services;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private IAccountService _accountService;

    public AccountsController(IAccountService accountService)
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