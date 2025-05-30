using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Transaction
{
    public class UpdateTransactionModel
    {
        [Required(ErrorMessage = "Введите сумму.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Введите описание.")]
        [StringLength(100, ErrorMessage = "Слишком длинное описание.")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int AccountId { get; set; }

        public int CategoryId { get; set; }
    }
}
