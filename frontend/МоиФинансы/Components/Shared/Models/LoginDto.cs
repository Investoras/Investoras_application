namespace МоиФинансы.Components.Shared.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [Required(ErrorMessage = "Email является обязательным полем.")]
        [EmailAddress(ErrorMessage = "Некорректный формат email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль является обязательным полем.")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} и не более {1} символов.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
