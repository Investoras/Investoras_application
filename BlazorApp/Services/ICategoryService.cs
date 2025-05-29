using BlazorApp.Models.Category;

namespace BlazorApp.Services
{
    public interface ICategoryService
    {
        Task<HttpResponseMessage> AddAsync(CreateCategoryModel category);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<List<CategoryModel>> GetAllAsync();
        Task<CategoryModel?> GetByIdAsync(int id);
        Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryModel category);
    }
}