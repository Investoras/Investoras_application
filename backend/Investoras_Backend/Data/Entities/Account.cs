using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Investoras_Backend.Data.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
