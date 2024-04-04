using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories
{
    public class CourseAutoMapper
    {
        public static CourseEntity ToCourseEntity(CourseDto dto)
        {
            if(dto != null)
            {
                var newEntity = new CourseEntity
                {
                    Title = dto.Title,
                    Author = dto.Author,    
                    Price = dto.Price,
                    Hours = dto.Hours,
                    PictureUrl = dto.PictureUrl,
                    IsBestSeller = bool.Parse(dto.IsBestSeller!),
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice
                };

                return newEntity;
            }
            return null!;
        }

        public static CourseEntity ToCourseEntity(UpdateCourseDto dto)
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
                    IsBestSeller = bool.Parse(dto.IsBestSeller!),
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice
                };

                return newEntity;
            }
            return null!;
        }

        public static UpdateCourseDto ToUpdateCourseDto(CourseEntity entity)
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
                    IsBestSeller = entity.IsBestSeller.ToString(),
                    LikesInNumbers = entity.LikesInNumbers,
                    LikesInProcent = entity.LikesInProcent,
                    DiscountPrice = entity.DiscountPrice
                };

                return newDto;
            }
            return null!;
        }
    }
}
