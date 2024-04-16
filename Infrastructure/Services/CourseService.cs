using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.Diagnostics;


namespace Infrastructure.Services
{
    public class CourseService(CourseRepository coursesRepository, CategoryService categoryService)
    {
        private readonly CourseRepository _courseRepository = coursesRepository;
        private readonly CategoryService _categoryService = categoryService;

        public async Task<CourseEntity> CreateCourseAsync(CourseDto newCourse, string categoryName)
        {
            try
            {
                if (newCourse != null)
                {
                    if (!await _courseRepository.Exists(course => course.Author == newCourse.Author && course.Title == newCourse.Title))
                    {
                        var category = await _categoryService.GetCategoryEntity(categoryName);
                        var course = CourseAutoMapper.ToCourseEntity(newCourse, category.Id);
                        var result = await _courseRepository.AddToDb(course);
                        if (result != null)
                            return course;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<CourseResult> GetAllCoursesAsync(string category = "", string searchValue = "", int pageNumber = 1, int pageSize = 10)
        {
            List<CourseDto> courseList = [];
            var response = new CourseResult();
            try
            {
                var courseEntities = await _courseRepository.GetAll(category, searchValue);

                if (courseEntities.Any())
                {
                    foreach (var entity in courseEntities)
                    {
                        var course = CourseAutoMapper.ToCourseDto(entity);
                        courseList.Add(course);
                    }

                    response.Succeeded = true;
                    response.TotalItems = courseEntities.Count();
                    response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
                    response.Courses = courseList.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();
                    return response;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<List<CourseDto>> GetAllCoursesByIdsAsync(List<int> ids)
        {
            try
            {
                var courses = await _courseRepository.GetAllByIds(ids);

                if (courses.Any())
                {         
                    List<CourseDto> returnList = []; 

                    foreach(var course in courses)
                    {
                        returnList.Add(CourseAutoMapper.ToCourseDto(course));
                    }
                    return returnList;
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
                    var category = await _categoryService.GetCategoryEntity(newValues.Category!);
                    if (category != null)
                    {
                        var result = await _courseRepository.UpdateEntity(CourseAutoMapper.ToCourseEntity(newValues, category.Id), c => c.Id == newValues.Id);
                        if (result != null)
                        {
                            return CourseAutoMapper.ToUpdateCourseDto(result, category.Id);
                        }
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
