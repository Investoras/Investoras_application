using Investoras_Backend.Data;
using Investoras_Backend.Models;
using Investoras_Backend.Models.Updaters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public AccountsController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAllAccounts()
    {
        var allAccounts = dbContext.Accounts.ToList();
        return Ok(allAccounts);
    }
    [HttpPost]
    public IActionResult AddAccount(UpdateAccount AddAccount)
    {
        var account = new Account()
        {
            Name = AddAccount.Name,
            Balance = AddAccount.Balance,
            UserId = AddAccount.UserId,
            CreatedAt = DateTime.UtcNow
        };


        dbContext.Accounts.Add(account);
        dbContext.SaveChanges();
        return Ok(account);
    }
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateAccount(int id, UpdateAccount UpdateAccount)
    {
        var account = dbContext.Accounts.Find(id);
        if (account == null)
        {
            return NotFound();
        }

        account.Name = UpdateAccount.Name;
        account.Balance = UpdateAccount.Balance;
        account.UserId = UpdateAccount.UserId;
        account.CreatedAt = DateTime.UtcNow;
        dbContext.SaveChanges();
        return Ok(account);
    }
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteAccount(int id)
    {
        var account = dbContext.Accounts.Find(id);

        if (account == null)
        {
            return NotFound();
        }

        dbContext.Accounts.Remove(account);
        dbContext.SaveChanges();
        return Ok();
    }
}
