using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; init; }
        // Другие поля...
    }
}
