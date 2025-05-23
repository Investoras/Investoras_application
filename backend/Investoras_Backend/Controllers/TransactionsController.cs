using ClassLibrary.Dto.Transaction;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Controllers;

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


    [HttpGet("Expenses")]
    public async Task<IActionResult> GetSumOfExpensesByAccountId(int id, CancellationToken cancellationToken)
    {
        var expenses = await _transactionService.GetSumOfExpensesByAccountId(id, cancellationToken);
        return Ok(expenses);
    }

    [HttpGet("Incomes")]
    public async Task<IActionResult> GetSumOfIncomeByAccountId(int id, CancellationToken cancellationToken)
    {
        var expenses = await _transactionService.GetSumOfIncomeByAccountId(id, cancellationToken);
        return Ok(expenses);
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction(CreateTransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.CreateTransaction(transactionDto, cancellationToken);
        return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
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
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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
    [HttpGet("LastFive")]
    public async Task<IActionResult> GetLastFiveTransactionsByAccountId(int id, CancellationToken cancellationToken)
    {
        var allTransactions = await _transactionService.GetLastFiveTransactionsByAccountId(id,cancellationToken);
        return Ok(allTransactions);
    }
    [HttpGet("AllByAccountId")]
    public async Task<IActionResult> GetAllTransactionsByAccoutnId(int id, CancellationToken cancellationToken)
    {
        var allTransactions = await _transactionService.GetAllTransactionsByAccoutnId(id, cancellationToken);
        return Ok(allTransactions);
    }
}