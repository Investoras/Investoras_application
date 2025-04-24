using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Investoras_Backend.Data;
using Investoras_Backend.Data.Entities;
using static System.Net.Mime.MediaTypeNames;
using Investoras_Backend.Data.Dto;

namespace Investoras_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TransactionsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transactions = dbContext.Transactions.ToList();
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult AddTransaction(TransactionDto AddTransaction)
        {
            var transaction = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = AddTransaction.Amount,
                Description = AddTransaction.Description,
                CategoryId = AddTransaction.CategoryId
            };

            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            return Ok(transaction);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateTransaction(int id, TransactionDto UpdateTransaction)
        {
            var transaction = dbContext.Transactions.Find(id);

            if (transaction == null)
                return NotFound();

            transaction.Description = UpdateTransaction.Description;
            transaction.CategoryId = UpdateTransaction.CategoryId;
            transaction.Amount = UpdateTransaction.Amount;
            transaction.Date = DateTime.UtcNow;

            dbContext.SaveChanges();
            return Ok(transaction);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = dbContext.Transactions.Find(id);

            if (transaction == null)
                return NotFound();

            dbContext.Transactions.Remove(transaction);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
