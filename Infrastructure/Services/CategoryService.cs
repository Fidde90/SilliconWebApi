using Infrastructure.Dtos;
using Infrastructure.Entities;
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

        public async Task<CategoryEntity> GetCategoryEntity(string categoryName)
        {
            try
            {
                if (await _categoryRepository.Exists(c => c.CategoryName == categoryName))
                {
                    var category = await _categoryRepository.GetOne(c => c.CategoryName == categoryName);
                    return category;
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
                        var created = await _categoryRepository.AddToDb(category);

                        if (created != null)
                            return true;
                    }                
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return false;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            if (id >= 0)
            {
                try
                {
                    if (await _categoryRepository.Exists(c => c.Id == id))
                    {
                        var deleted = await _categoryRepository.DeleteFromDb(c => c.Id == id);                  
                        if (deleted)
                            return true;
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return false;
        }
    }
}
