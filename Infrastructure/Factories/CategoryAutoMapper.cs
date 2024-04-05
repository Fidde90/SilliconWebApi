using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories
{
    public static class CategoryAutoMapper
    {
        public static CategoryDto ToCategoryDto(CategoryEntity entity)
        {
            if(entity != null)
            {
                var newDto = new CategoryDto
                {
                    Id = entity.Id,
                    CategoryName = entity.CategoryName
                };

                return newDto;
            }
            return null!;
        }
    }
}
