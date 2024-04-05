using Infrastructure.Dtos;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class CategoryService(CategoryRepository categoryRepository)
    {
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            List<CategoryDto> Dtos = [];

            try
            {
                var categoryEntities = await _categoryRepository.GetAll();

                if (categoryEntities.Any())
                {
                    foreach (var category in categoryEntities)
                    {
                        var catagory = CategoryAutoMapper.ToCategoryDto(category);
                        Dtos.Add(catagory);
                    }

                    return Dtos;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
