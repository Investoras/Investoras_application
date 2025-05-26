using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.Category;
using Investoras_Backend.Data.Entities;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ClassLibrary.Dto.Account;
using System.ComponentModel.DataAnnotations;

namespace Investoras_Backend.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
    Task<CategoryDto> GetCategoryById(int id, CancellationToken cancellationToken);
    Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken);
    Task UpdateCategory(int id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken);
    Task DeleteCategory(int id, CancellationToken cancellationToken);
}
public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CategoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryModel = new CategoryModel
        {
            Description = categoryDto.Description,
            IsIncome = categoryDto.IsIncome,
            Name = categoryDto.Name
        };

        var validationContext = new ValidationContext(categoryModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            categoryModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }

        var entity = _mapper.Map<Category>(categoryModel);
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id,cancellationToken);
        if (category == null) throw new NotFoundException("Категория не найдена");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
    {
        var allCategories = await _context.Categories.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CategoryDto>>(allCategories);
    }

    public async Task<CategoryDto> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id,cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task UpdateCategory(int id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryModel = new CategoryModel
        {
            Description = categoryDto.Description,
            IsIncome = categoryDto.IsIncome,
            Name = categoryDto.Name
        };

        var validationContext = new ValidationContext(categoryModel);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            categoryModel,
            validationContext,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            // Создаем исключение с информацией об ошибках
            var errors = validationResults
                .SelectMany(vr => vr.MemberNames.Select(mn => new { Member = mn, Error = vr.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Error).ToArray());

            throw new Exceptions.ValidationException("Ошибка валидации", errors);
        }
        var category = await _context.Categories.FindAsync(id, cancellationToken);
        if (category == null) throw new NotFoundException("Категория не найдена");
        categoryModel.CategoryId = category.CategoryId;

        _mapper.Map(categoryModel, category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
