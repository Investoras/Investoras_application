using System.Transactions;

namespace Investoras_Backend.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
