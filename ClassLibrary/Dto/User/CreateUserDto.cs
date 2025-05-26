using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto.User
{
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        
        public string Password { get; set; } = string.Empty;
    }
}

