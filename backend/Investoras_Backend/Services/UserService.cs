using AutoMapper;
using Investoras_Backend.Data;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    Task<UserDto> GetUserById(int id, CancellationToken cancellationToken);
    Task<UserDto> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken);
    Task UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
    Task<UserDto> LoginUser(UserDto userDto, CancellationToken cancellationToken);
}
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<UserDto> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userDto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(user);
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

    public async Task<UserDto> LoginUser(UserDto userDto, CancellationToken cancellationToken)
    {
        Console.WriteLine("Sending login request...");
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userDto.Username
                                  && u.Password == userDto.Password);
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null) throw new NotFoundException("User not found");

        _mapper.Map(userDto, user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
