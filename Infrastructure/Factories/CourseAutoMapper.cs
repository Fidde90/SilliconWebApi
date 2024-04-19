using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Infrastructure.Factories
{
    public static class CourseAutoMapper
    {
        public static CourseDto ToCourseDto(CourseEntity entity)
        {
            if (entity != null)
            {
                var Dto = new CourseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Author = entity.Author,
                    Price = entity.Price,
                    Hours = entity.Hours,
                    PictureUrl = entity.PictureUrl,
                    IsBestSeller = entity.IsBestSeller,
                    IsDigital = entity.IsDigital,
                    LikesInNumbers = entity.LikesInNumbers,
                    LikesInProcent = entity.LikesInProcent,
                    DiscountPrice = entity.DiscountPrice,
                    Category = entity.Category!.CategoryName
                };

                return Dto;
            }
            return null!;
        }

        public static CourseEntity ToCourseEntity(CourseDto dto, int categoryId)
        {
            if (dto != null)
            {
                var newEntity = new CourseEntity
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Price = dto.Price,
                    Hours = dto.Hours,
                    PictureUrl = dto.PictureUrl,
                    IsBestSeller = dto.IsBestSeller!,
                    IsDigital = dto.IsDigital,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice,
                    CategoryId = categoryId
                };

                return newEntity;
            }
            return null!;
        }

        public static CourseEntity ToCourseEntity(UpdateCourseDto dto, int categoryId)
        {
            if (dto != null)
            {
                var newEntity = new CourseEntity
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Author = dto.Author,
                    Price = dto.Price,
                    Hours = dto.Hours,
                    PictureUrl = dto.PictureUrl,
                    IsBestSeller = dto.IsBestSeller!,
                    IsDigital = dto.IsDigital,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice,
                    LastUpdated = dto.LastUpdated,
                    CategoryId = categoryId
                };

                return newEntity;
            }
            return null!;
        }

        public static UpdateCourseDto ToUpdateCourseDto(CourseEntity entity, int categoryId)
        {
            if (entity != null)
            {
                var newDto = new UpdateCourseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Author = entity.Author,
                    Price = entity.Price,
                    Hours = entity.Hours!,
                    PictureUrl = entity.PictureUrl!,
                    IsBestSeller = entity.IsBestSeller!,
                    IsDigital = entity.IsDigital,
                    LikesInNumbers = entity.LikesInNumbers,
                    LikesInProcent = entity.LikesInProcent,
                    DiscountPrice = entity.DiscountPrice,
                    LastUpdated = entity.LastUpdated,
                    CategoryId = entity.CategoryId,                 
                };

                return newDto;
            }
            return null!;
        }
    }
}
