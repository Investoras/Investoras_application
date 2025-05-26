using ClassLibrary.Dto.Transaction;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Microsoft.AspNetCore.Authorization;

namespace Investoras_Backend.Controllers;

//[Authorize]
[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase

{
    private ITransactionService _transactionService;

    public TransactionController(ITransactionService userService)
    {
        _transactionService = userService;
    }
    [HttpGet("All")]
    public async Task<ActionResult> GetAllTransactions(CancellationToken cancellationToken)
    {
        var allTransactions = await _transactionService.GetAllTransactions(cancellationToken);
        return Ok(allTransactions);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(int id, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetTransactionById(id, cancellationToken);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }


    [HttpGet("Expenses/{id:int}")]
    public async Task<IActionResult> GetSumOfExpensesByAccountId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var expenses = await _transactionService.GetSumOfExpensesByAccountId(id, cancellationToken);
            return Ok(expenses);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
    }

    [HttpGet("Incomes/{id:int}")]
    public async Task<IActionResult> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken)
    {
        try { 
        var expenses = await _transactionService.GetSumOfIncomeByAccountId(id, cancellationToken);
        return Ok(expenses);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _transactionService.CreateTransaction(transactionDto, cancellationToken);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
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
    public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        try
        {
            await _transactionService.UpdateTransaction(id, transactionDto, cancellationToken);
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
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteTransaction(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _transactionService.DeleteTransaction(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("LastFive/{id:int}")]
    public async Task<IActionResult> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken)
    {
        try { 
        var allTransactions = await _transactionService.GetLastFiveTransactionsByAccountId(id,cancellationToken);
        return Ok(allTransactions);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
    }
    [HttpGet("AllByAccountId/{id:int}")]
    public async Task<IActionResult> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken)
    {
        try { 
        var allTransactions = await _transactionService.GetAllTransactionsByAccoutnId(id, cancellationToken);
        return Ok(allTransactions);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
    }
}