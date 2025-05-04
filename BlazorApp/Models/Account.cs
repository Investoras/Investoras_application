using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
