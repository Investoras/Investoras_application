using AutoMapper;
using Investoras_Backend.Data;
using ClassLibrary.Dto.Category;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> GetAllCategories(CancellationToken cancellationToken);
    Task<CategoryModel> GetCategoryById(int id, CancellationToken cancellationToken);
    Task<CategoryModel> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken);
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
    public async Task<CategoryModel> CreateCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryModel = CategoryModel.Create(categoryDto.Name, categoryDto.IsIncome, categoryDto.Description);
        var entity = _mapper.Map<Category>(categoryModel);
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CategoryModel>(entity);
    }

    public async Task DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id,cancellationToken);
        if (category == null) throw new NotFoundException("Категория не найдена");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CategoryModel>> GetAllCategories(CancellationToken cancellationToken)
    {
        var allCategories = await _context.Categories.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CategoryModel>>(allCategories);
    }

    public async Task<CategoryModel> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id,cancellationToken);
        return _mapper.Map<CategoryModel>(category);
    }

    public async Task UpdateCategory(int id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id, cancellationToken);
        if (category == null) throw new NotFoundException("Категория не найдена");

        _mapper.Map(categoryDto, category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
