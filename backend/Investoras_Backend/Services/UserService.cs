﻿using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.User;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using ClassLibrary.Dto;

namespace Investoras_Backend.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllUsers(CancellationToken cancellationToken);
    Task<UserModel> GetUserById(int id, CancellationToken cancellationToken);
    Task<UserModel> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken);
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
    public async Task<UserModel> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        bool usernameExists = await _context.Users
        .AnyAsync(u => u.Username == userDto.Username);
        if (usernameExists)
        {
            throw new ValidationException("Имя пользователя уже занято.");
        }
        var userModel = UserModel.Create(userDto.Username,userDto.Email,userDto.Password);
        var entity = _mapper.Map<User>(userModel);
        _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserModel>(entity);
    }

    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null) throw new NotFoundException("User not found");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers(CancellationToken cancellationToken)
    {
        var allUsers = await _context.Users.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserModel>>(allUsers);
    }

    public async Task<UserModel> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        return _mapper.Map<UserModel>(user);
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
        bool usernameExists = await _context.Users
            .AnyAsync(u => u.Username == userDto.Username && u.UserId != id);
        if (usernameExists)
            throw new ValidationException("Имя пользователя уже занято.");

        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user == null) throw new NotFoundException("Пользователь не найден");

        _mapper.Map(userDto, user);

        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            user.Password = UserModel.HashPassword(userDto.Password);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
