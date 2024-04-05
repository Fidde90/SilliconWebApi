using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SilliconWebApi.Controllers
{
    public class CategoryController(CategoryService categoryservice) : Controller
    {
        private readonly CategoryService _categoryService = categoryservice;

        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();

                if(categories.Any())
                    return Ok(categories);

                return NotFound();
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return StatusCode(500, "An unexpected error occurred. Please contact the administrator.");
        }


    }
}
