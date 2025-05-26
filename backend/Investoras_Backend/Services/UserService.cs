using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.User;
using Investoras_Backend.Data.Entities;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using ClassLibrary.Dto;
using ClassLibrary.Dto.Account;

namespace Investoras_Backend.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    Task<UserDto> GetUserById(int id, CancellationToken cancellationToken);
    Task<UserDto> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken);
    Task UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
    Task<AuthResponseDto> LoginUser(LoginUserDto userDto, CancellationToken cancellationToken);
}
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    public UserService(ApplicationDbContext context, IMapper mapper, ITokenService tokenService)
    {
        _context = context;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    public async Task<UserDto> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var userModel = new UserModel
        {
             CreatedAt = DateTime.UtcNow,
             Email = userDto.Email,
             Password = userDto.Password,
             Username = userDto.Username
        };

        var validationContext = new ValidationContext(userModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            userModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }
        bool usernameExists = await _context.Users
        .AnyAsync(u => u.Username == userDto.Username);
        if (usernameExists)
        {
            throw new ValidationException("Имя пользователя уже занято.");
        }
        userModel.Password = UserModel.HashPassword(userModel.Password);
        var entity = _mapper.Map<User>(userModel);
        _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(entity);
    }

    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null) throw new NotFoundException("User not found");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        var allUsers = await _context.Users.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserDto>>(allUsers);
    }

    public async Task<UserDto> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<AuthResponseDto> LoginUser(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userDto.Username);

        if (user == null) throw new NotFoundException("Пользователь не найден");

        if (!UserModel.VerifyPassword(userDto.Password, user.Password))
            throw new NotFoundException("Неправильный пароль");

        var userModel = _mapper.Map<UserModel>(user);
        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        var userModel = new UserModel
        {
            Email = userDto.Email,
            Password = userDto.Password,
            Username = userDto.Username
        };

        var validationContext = new ValidationContext(userModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            userModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }
        bool usernameExists = await _context.Users
            .AnyAsync(u => u.Username == userDto.Username && u.UserId != id);
        if (usernameExists)
            throw new ValidationException("Имя пользователя уже занято.");

        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null) throw new NotFoundException("Пользователь не найден");
        userModel.UserId = user.UserId;
        userModel.CreatedAt = user.CreatedAt;
        userModel.Password = UserModel.HashPassword(userModel.Password);

        _mapper.Map(userModel, user);

        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            user.Password = UserModel.HashPassword(userDto.Password);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
