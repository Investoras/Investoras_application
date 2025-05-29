using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Transaction
{
    public class CreateTransactionModel
    {
        [Required(ErrorMessage = "Введите сумму.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Введите описание.")]
        [StringLength(100, ErrorMessage = "Слишком длинное описание.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Задайте счет.")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Задайте счет.")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Задайте категорию.")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Задайте категорию.")]
        public int CategoryId { get; set; }
    }
}
