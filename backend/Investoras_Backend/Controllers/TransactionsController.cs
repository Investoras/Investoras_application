using Investoras_Backend.Data.Dto;
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
}