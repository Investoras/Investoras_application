using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.User
{
    public class UpdateUserModel
    {
        [StringLength(20, ErrorMessage = "Имя пользователя не должно превышать 20 символов.")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Введён некорректный адрес электронной почты.")]
        public string? Email { get; set; }

        [PasswordNotEmptyAttribute]
        public string? Password { get; set; }
    }
}

