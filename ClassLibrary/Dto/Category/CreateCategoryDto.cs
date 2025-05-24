using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Введите название категории.")]
        [StringLength(30, ErrorMessage = "Слишком длинное название категории.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите тип категории.")]
        public bool IsIncome { get; set; }

        [Required(ErrorMessage = "Введите описание категории.")]
        [StringLength(100, ErrorMessage = "Слишком длинное описание категории.")]
        public string Description { get; set; } = string.Empty;
    }
}
