using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class CourseService(CourseRepository coursesRepository)
    {
        private readonly CourseRepository _courseRepository = coursesRepository;

        public async Task<CourseEntity> CreateCourseAsync()
        {
            var course = new CourseEntity();

            return course;
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
