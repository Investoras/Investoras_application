<<<<<<< HEAD
﻿using ClassLibrary.Dto.Category;
=======
﻿using ClassLibrary.Dto;

>>>>>>> _piechart

namespace BlazorApp.Services
{
    public interface ICategoryService
    {
<<<<<<< HEAD
        Task<HttpResponseMessage> AddAsync(CreateCategoryDto category);
        Task<HttpResponseMessage> UpdateAsync(int id, UpdateCategoryDto category);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
    }
}
=======
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task AddCategoryAsync(CreateCategoryDto dto);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
>>>>>>> _piechart
