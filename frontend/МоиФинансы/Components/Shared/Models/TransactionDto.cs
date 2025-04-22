namespace МоиФинансы.Components.Shared.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TransactionDto
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Required]
        public string TransactionType { get; set; } // "Income" или "Expense"

        public bool IsIncome => TransactionType == "Income";
    }
}


