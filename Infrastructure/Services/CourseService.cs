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
                if(newCourse != null)
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
        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync()
        {
            try
            {
                var courseList = await _courseRepository.GetAll();
                if (courseList.Count() > 0)
                    return courseList;
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
    }
}
