using System.ComponentModel.DataAnnotations;

public class PasswordNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;

        if (string.IsNullOrEmpty(password))
        {
            return ValidationResult.Success;
        }

        if (password.Length < 6 || password.Length > 20)
        {
            return new ValidationResult("Пароль должен содержать от 6 до 20 символов.");
        }

        var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$");
        if (!regex.IsMatch(password))
        {
            return new ValidationResult("Пароль должен содержать хотя бы одну заглавную латинскую букву, цифру и специальные символы @,$,!,%,*,?,&.");
        }

        return ValidationResult.Success;
    }
}
