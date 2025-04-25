using Investoras_Backend.Data.Entities;
using System.ComponentModel.DataAnnotations;
namespace Investoras_Backend.Data.Dto;

public record AccountDto(
    int AccountId,
    string Name,
    decimal Balance,
    int UserId
    );
public record CreateAccountDto(
    [Required(ErrorMessage = "Введите название.")]
    [StringLength(30, ErrorMessage = "Слишком длинное название.")]
    string Name,

    [Required(ErrorMessage = "Введите баланс.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Баланс должен быть больше 0.")]
    decimal Balance,

    int UserId
    );
public record UpdateAccountDto(
    [Required(ErrorMessage = "Введите название.")]
    [StringLength(30, ErrorMessage = "Слишком длинное название.")]
    string Name,

    [Required(ErrorMessage = "Введите баланс.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Баланс должен быть больше 0.")]
    decimal Balance,

    int UserId
    );