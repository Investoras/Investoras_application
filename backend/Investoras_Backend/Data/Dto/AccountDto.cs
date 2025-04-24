using Investoras_Backend.Data.Entities;

namespace Investoras_Backend.Data.Dto;

public record AccountDto(
    int AccountId,
    string Name,
    decimal Balance,
    int UserId
    );
public record CreateAccountDto(
    string Name,
    decimal Balance,
    int UserId
    );
public record UpdateAccountDto(
    string Name,
    decimal Balance,
    int UserId
    );