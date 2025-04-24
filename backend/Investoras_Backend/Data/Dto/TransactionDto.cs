using Investoras_Backend.Data.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Investoras_Backend.Data.Dto;

public record TransactionDto(
    int TransactionId,
    decimal Amount,
    string Description,
    int AccountId,
    int CategoryId
    );
public record CreateTransactionDto(
    decimal Amount,
    string Description,
    int AccountId,
    int CategoryId
    );
public record UpdateDto(
    decimal Amount,
    string Description,
    int AccountId,
    int CategoryId
    );