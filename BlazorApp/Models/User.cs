using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; init; }
        // Другие поля...
    }
}
