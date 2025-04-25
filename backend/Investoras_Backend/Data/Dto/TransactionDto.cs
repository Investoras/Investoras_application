using Investoras_Backend.Data.Entities;
using System.ComponentModel.DataAnnotations;
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
    [Required(ErrorMessage = "Введите сумму.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
    decimal Amount,

    [Required(ErrorMessage = "Введите описание.")]
    [StringLength(100, ErrorMessage = "Слишком длинное описание.")]
    string Description,

    int AccountId,

    int CategoryId
    );

public record UpdateDto(
    [Required(ErrorMessage = "Введите сумму.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
    decimal Amount,

    [Required(ErrorMessage = "Введите описание.")]
    [StringLength(100, ErrorMessage = "Слишком длинное описание.")]
    string Description,

    int AccountId,

    int CategoryId
    );