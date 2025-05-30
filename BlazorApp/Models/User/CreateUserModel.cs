using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.User
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Задайте имя пользователя.")]
        [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Задайте адрес электронной почты.")]
        [EmailAddress(ErrorMessage = "Введён некорректный адрес электронной почты.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Задайте пароль.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен содержать не менее 6 символов.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Пароль должен содержать хотя бы " +
            "одну заглавную латинскую букву, цифру и специальные символы @,$,!,%,*,?,&.")]
        public string? Password { get; set; }
    }
}

