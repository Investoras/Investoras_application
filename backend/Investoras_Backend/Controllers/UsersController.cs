using Investoras_Backend.Data;
using Investoras_Backend.Models;
using Investoras_Backend.Models.Updaters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public UsersController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var allUsers = dbContext.Users.ToList();
        return Ok(allUsers);
    }
    [HttpPost]
    public IActionResult AddUser(UpdateUser AddUser)
    {
        var user = new User()
        {
            Username = AddUser.Username,
            Email = AddUser.Email,
            Password = AddUser.Password,
            CreatedAt = DateTime.UtcNow
        };


        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return Ok(user);
    }
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateUser(int id, UpdateUser UpdateUser)
    {
        var user = dbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Username = UpdateUser.Username;
        user.Password = UpdateUser.Password;
        user.Email = UpdateUser.Email;
        user.CreatedAt = DateTime.UtcNow;
        dbContext.SaveChanges();
        return Ok(user);
    }
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        var user = dbContext.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
        return Ok();
    }
}


