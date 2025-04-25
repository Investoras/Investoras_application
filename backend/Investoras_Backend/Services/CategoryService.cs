using AutoMapper;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data;
using SendGrid.Helpers.Errors.Model;
using Microsoft.EntityFrameworkCore;

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
        var category = _mapper.Map<Category>(categoryDto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) throw new NotFoundException("Product not found");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
    {
        var allCategories = await _context.Categories.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CategoryDto>>(allCategories);
    }

    public async Task<CategoryDto> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task UpdateCategory(int id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) throw new NotFoundException("Product not found");

        _mapper.Map(categoryDto, category);
        await _context.SaveChangesAsync();
    }
}
