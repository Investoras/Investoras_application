using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto.User
{
    public class UpdateUserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        
        public string Password { get; set; }
    }
}

