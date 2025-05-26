using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto.Transaction
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public int AccountId { get; set; }

        public int CategoryId { get; set; }
    }
}
