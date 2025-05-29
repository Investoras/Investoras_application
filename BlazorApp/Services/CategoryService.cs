using ClassLibrary.Dto.Category;
using System.Net.Http.Json;
using BlazorApp.Models.Category;
using BlazorApp.Mappings;

namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<CategoryDto>>("Category/All")
                       ?? new List<CategoryDto>();
            return dtos.ToModel();
        }

        public async Task<CategoryModel?> GetByIdAsync(int id)
        {
            var dto = await _http.GetFromJsonAsync<CategoryDto>($"Category/{id}");
            return dto?.ToModel();
        }

        public async Task<HttpResponseMessage> AddAsync(CreateCategoryModel category)
        {
            var dto = category.ToDto();
            return await _http.PostAsJsonAsync("Category", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryModel category)
        {
            var dto = category.ToDto();
            return await _http.PutAsJsonAsync($"Category/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"Category/{id}");
        }
    }
}
