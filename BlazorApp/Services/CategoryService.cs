using ClassLibrary.Dto.Category;
using System.Net.Http.Json;
using ClassLibrary.Dto;


namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<CategoryDto>>("Category/All")
               ?? new List<CategoryDto>();

        public async Task<CategoryDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<CategoryDto>($"Category/{id}");

        public async Task<HttpResponseMessage> AddAsync(CreateCategoryDto category)
            => await _http.PostAsJsonAsync("Category", category);

        public async Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryDto category)
            => await _http.PutAsJsonAsync($"Category/{id}", category);

        public async Task<HttpResponseMessage> DeleteAsync(int id)
            => await _http.DeleteAsync($"Category/{id}");
        }

}
