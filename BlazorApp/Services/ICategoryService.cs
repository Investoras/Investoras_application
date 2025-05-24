using ClassLibrary.Dto.Category;

namespace BlazorApp.Services
{
    public interface ICategoryService
    {
        Task<HttpResponseMessage> AddAsync(CreateCategoryDto category);
        Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryDto category);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
    }
}