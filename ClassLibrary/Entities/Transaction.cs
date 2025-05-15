using ClassLibrary.Entities;

namespace ClassLibrary.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public Account Account { get; set; }
        public Category Category { get; set; }
    }
}
