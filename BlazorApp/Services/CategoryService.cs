<<<<<<< HEAD
﻿using ClassLibrary.Dto.Category;
using System.Net.Http.Json;
=======
﻿using System.Net.Http.Json;
using ClassLibrary.Dto;

>>>>>>> _piechart

namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

<<<<<<< HEAD
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<IEnumerable<CategoryDto>>("https://localhost:7214/Category/All")
               ?? Enumerable.Empty<CategoryDto>();

        public async Task<CategoryDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<CategoryDto>($"https://localhost:7214/Category/{id}");

        public async Task<HttpResponseMessage> AddAsync(CreateCategoryDto category)
            => await _http.PostAsJsonAsync("https://localhost:7214/Category", category);

        public async Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryDto category)
            => await _http.PutAsJsonAsync($"https://localhost:7214/Category/{id}", category);

        public async Task<HttpResponseMessage> DeleteAsync(int id)
            => await _http.DeleteAsync($"https://localhost:7214/Category/{id}");
    }

=======
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoryDto>>("https://localhost:7214/Category/All");
        }
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CategoryDto>($"https://localhost:7214/Category/{id}");
        }
        public async Task AddCategoryAsync(CreateCategoryDto dto)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7214/Category", dto);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var response = await _http.PutAsJsonAsync($"https://localhost:7214/Category/{id}", dto);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7214/Category/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
>>>>>>> _piechart
}
