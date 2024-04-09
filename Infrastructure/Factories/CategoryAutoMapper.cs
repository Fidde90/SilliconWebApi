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

        public static CategoryEntity ToCategoryEntity(CategoryDto dto)
        {
            if (dto != null)
            {
                var newEntity = new CategoryEntity
                {
                    Id = dto.Id,
                    CategoryName = dto.CategoryName
                };

                return newEntity;
            }
            return null!;
        }

        public static CategoryEntity ToCategoryEntity(CreateCategoryDto dto)
        {
            if (dto != null)
            {
                var newEntity = new CategoryEntity
                {             
                    CategoryName = dto.CategoryName
                };

                return newEntity;
            }
            return null!;
        }
    }
}
