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

        public async Task<bool> CreateCategory(CreateCategoryDto newCategory)
        {
            if(newCategory != null)
            {
                try
                {
                    if(!await _categoryRepository.Exists(c => c.CategoryName == newCategory.CategoryName))
                    {
                        var category = CategoryAutoMapper.ToCategoryEntity(newCategory);
                        var created = _categoryRepository.AddToDb(category);

                        if (created != null)
                            return true;
                    }                
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return false;
        }
    }
}
