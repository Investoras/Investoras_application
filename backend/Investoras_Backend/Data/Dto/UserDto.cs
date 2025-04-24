namespace Investoras_Backend.Data.Dto;

public record UserDto(
    int UserId,
    string Username,
    string Email,
    string Password,
    DateTime CreatedAt
    );

public record CreateUserDto(
    string Username,
    string Email,
    string Password,
    DateTime CreatedAt
    );
public record UpdateUserDto(
    string Username,
    string Email,
    string Password,
    DateTime CreatedAt
    );