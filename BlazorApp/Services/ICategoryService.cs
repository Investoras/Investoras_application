using ClassLibrary.Dto;


namespace BlazorApp.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task AddCategoryAsync(CreateCategoryDto dto);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
