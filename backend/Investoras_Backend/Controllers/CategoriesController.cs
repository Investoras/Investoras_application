using ClassLibrary.Dto.Category;
using Investoras_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Investoras_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet("All")]
    public async Task<ActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var allCategories = await _categoryService.GetAllCategories(cancellationToken);
        return Ok(allCategories);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryById(id, cancellationToken);
        if (category == null) return NotFound();
        return Ok(category);
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryService.CreateCategory(categoryDto, cancellationToken);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }
        catch (Exceptions.ValidationException ex)
        {
            // Преобразуем исключение в ModelState
            foreach (var error in ex.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }
            return BadRequest(ModelState);
        }
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        try
        {
            await _categoryService.UpdateCategory(id, categoryDto, cancellationToken);
            return NoContent();
        }
        catch (Exceptions.ValidationException ex)
        {
            // Преобразуем исключение в ModelState
            foreach (var error in ex.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }
            return BadRequest(ModelState);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _categoryService.DeleteCategory(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
