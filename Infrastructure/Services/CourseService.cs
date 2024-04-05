using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.Diagnostics;


namespace Infrastructure.Services
{
    public class CourseService(CourseRepository coursesRepository)
    {
        private readonly CourseRepository _courseRepository = coursesRepository;

        public async Task<CourseEntity> CreateCourseAsync(CourseDto newCourse)
        {
            try
            {
                if (newCourse != null)
                {
                    if (!await _courseRepository.Exists(course => course.Author == newCourse.Author && course.Title == newCourse.Title))
                    {
                        var course = CourseAutoMapper.ToCourseEntity(newCourse);
                        var result = await _courseRepository.AddToDb(course);
                        if (result != null)
                            return course;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            List<CourseDto> Dtos = [];

            try
            {
                var courseEntities = await _courseRepository.GetAll();

                if (courseEntities.Any())
                {
                    foreach (var entity in courseEntities)
                    {
                        var course = CourseAutoMapper.ToCourseDto(entity);
                        Dtos.Add(course);
                    }

                    return Dtos;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<CourseEntity> GetOneCourseAsync(int id)
        {
            try
            {
                var course = await _courseRepository.GetOne(c => c.Id == id);
                if (course != null)
                    return course;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<UpdateCourseDto> UpdateCourseAsync(UpdateCourseDto newValues)
        {
            try
            {
                if (newValues != null && await _courseRepository.Exists(c => c.Id == newValues.Id))
                {
                    var result = await _courseRepository.UpdateEntity(CourseAutoMapper.ToCourseEntity(newValues), c => c.Id == newValues.Id);
                    if (result != null)
                    {
                        return CourseAutoMapper.ToUpdateCourseDto(result);
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                if (await _courseRepository.Exists(course => course.Id == id))
                {
                    var deleted = await _courseRepository.DeleteFromDb(course => course.Id == id);
                    if (deleted)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
