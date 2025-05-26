using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;

        public bool IsIncome { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
