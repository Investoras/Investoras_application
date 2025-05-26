using ClassLibrary.Dto.Account;
using ClassLibrary.Models;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using Investoras_Backend.Exceptions;

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
        try
        {
            var balance = await _accountService.GetTotalBalanceById(id, cancellationToken);
            return Ok(balance);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount(CreateAccountDto accountDto, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _accountService.CreateAccount(accountDto, cancellationToken);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
        }
        catch (Exceptions.ValidationException ex)
        {
            // Преобразуем исключение в ModelState
            foreach (var error in ex.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }
            return BadRequest(ModelState);
        }
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
        catch (Exceptions.ValidationException ex)
        {
            // Преобразуем исключение в ModelState
            foreach (var error in ex.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }
            return NotFound(ModelState);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex);
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