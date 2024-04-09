using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SilliconWebApi.Filters;
using System.Diagnostics;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [UseApiKey]
    [ApiController]
    public class CategoryController(CategoryService categoryService) : ControllerBase
    {
        private readonly CategoryService _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();

                if (categories.Any())
                    return Ok(categories);

                return NotFound();
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto newCategory)
        {
            if (ModelState.IsValid && newCategory.CategoryName.Length > 1)
            {
                try
                {
                    var result = await _categoryService.CreateCategory(newCategory);
                    if (result == true)
                        return Ok("Category created");

                    return Conflict();
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }

            return BadRequest();
        }
    }
}
