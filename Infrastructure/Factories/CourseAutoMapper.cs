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

    }
}
