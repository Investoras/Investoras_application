using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Data.Dto;

public record UserDto(
    int UserId,
    string Username,
    string Email,
    string Password,
    DateTime CreatedAt
    );

public record CreateUserDto(
    [Required(ErrorMessage = "Задайте имя пользователя.")]
    [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
    string Username,

    [Required(ErrorMessage = "Задайте адрес электронной почты.")]
    [EmailAddress(ErrorMessage = "Введён некорректный адрес электронной почты.")]
    string Email,

    [Required(ErrorMessage = "Задайте пароль.")]
    [StringLength(20, ErrorMessage = "Пароль должен содержать не менее 6 символов, хотя бы одну заглавную латинскую букву, цифру и специальные символы @,$,!,%,*,?,&.", MinimumLength = 6)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$")]
    string Password,
    DateTime CreatedAt
    );

public record UpdateUserDto(
    [Required(ErrorMessage = "Задайте имя пользователя.")]
    [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
    string Username,

    [Required(ErrorMessage = "Задайте адрес электронной почты.")]
    [EmailAddress(ErrorMessage = "Введён некорректный адрес электронной почты.")]
    string Email,

    [Required(ErrorMessage = "Задайте пароль.")]
    [StringLength(20, ErrorMessage = "Пароль должен содержать не менее 6 символов, хотя бы одну заглавную латинскую букву, цифру и специальные символы @,$,!,%,*,?,&.", MinimumLength = 6)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$")]
    string Password,
    DateTime CreatedAt
    );