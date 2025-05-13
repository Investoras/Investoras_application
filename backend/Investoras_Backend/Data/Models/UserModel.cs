using BCrypt.Net;
using Investoras_Backend.Data.Entities;
using Microsoft.CodeAnalysis.Scripting;
using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Data.Models
{
    public class UserModel
    {
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public static UserModel Create(string username, string email, string password)
        {
            return new UserModel
            {
                Username = username,
                Email = email,
                Password = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };
        }

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
