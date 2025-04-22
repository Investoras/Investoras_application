using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Investoras_Backend.Data;
using Investoras_Backend.Models;
using Investoras_Backend.Models.Updaters;
using static System.Net.Mime.MediaTypeNames;

namespace Investoras_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = dbContext.Categories;
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory(UpdateCategory AddCategory)
        {
            var category = new Category()
            {
                Name = AddCategory.Name,
                IsIncome = AddCategory.IsIncome,
                Description = AddCategory.Description
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return Ok(category);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCategory(int id, UpdateCategory UpdateCategory)
        {
            var category = dbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            category.Description = UpdateCategory.Description;
            category.Name = UpdateCategory.Name;
            category.IsIncome = UpdateCategory.IsIncome;
            
            dbContext.SaveChanges();
            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
