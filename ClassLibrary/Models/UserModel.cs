using BCrypt.Net;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models
{
    public record UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Задайте имя пользователя.")]
        [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Задайте адрес электронной почты.")]
        [EmailAddress(ErrorMessage = "Введён некорректный адрес электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Задайте пароль.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен содержать не менее 6 символов.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Пароль должен содержать не менее 6 символов, из них хотя бы одну заглавную латинскую букву, цифру и специальные символы @,$,!,%,*,?,&.")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public static bool VerifyPassword(string password,string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA384);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA384, 13);
        }

    }
}
