using ClassLibrary.Dto;
using ClassLibrary.Dto.User;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Investoras_Backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
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
        try
        {
            var user = await _userService.CreateUser(userDto, cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
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
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.UpdateUser(id, userDto, cancellationToken);
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
        catch(ValidationException ex)
        {
            return BadRequest(ex);
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
    public async Task<IActionResult> Login(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            var userModel = await _userService.LoginUser(userDto, cancellationToken);

            var userEntity = new User
            {
                UserId = userModel.UserId,
                Username = userModel.Username,
                Email = userModel.Email,
            };

            var token = _tokenService.GenerateToken(userEntity);

            var response = new AuthResponseDto
            {
                Token = token,
                UserId = userModel.UserId,
                Username = userModel.Username,
                Email = userModel.Email
            };

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

}


