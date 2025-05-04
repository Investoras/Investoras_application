﻿using Investoras_Backend.Data.Dto;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("All")]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var allUsers = await _userService.GetAllUsers(cancellationToken);
        return Ok(allUsers);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserById(id, cancellationToken);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUser(userDto, cancellationToken);
        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.UpdateUser(id, userDto, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.DeleteUser(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.LoginUser(userDto, cancellationToken);
        if (user == null)
            return Unauthorized();
        return Ok(user);
    }
}


