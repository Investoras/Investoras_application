using BlazorApp.Models.Category;
using ClassLibrary.Dto.Category;

namespace BlazorApp.Mappings
{
    public static class CategoryMapping
    {
        public static CategoryModel ToModel(this CategoryDto dto) => new()
        {
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            IsIncome = dto.IsIncome,
            Description = dto.Description
        };

        public static List<CategoryModel> ToModel(this List<CategoryDto> dtos)
            => dtos.Select(d => d.ToModel()).ToList();

        public static CreateCategoryDto ToDto(this CreateCategoryModel model) => new()
        {
            Name = model.Name,
            IsIncome = model.IsIncome,
            Description = model.Description
        };

        public static UpdateCategoryDto ToDto(this UpdateCategoryModel model) => new()
        {
            Name = model.Name,
            IsIncome = model.IsIncome,
            Description = model.Description
        };
    }
}
