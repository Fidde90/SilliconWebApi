using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    //[UseApiKey]
    [ApiController]
    public class CoursesController(CourseService coursesService) : ControllerBase
    {
        private readonly CourseService _coursesService = coursesService;


        #region User actions
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync(string category = "", string searchValue = "", int pageNumber = 1, int pageSize = 10)
        {
            var response = new CourseResult();

            try
            {
                var courseResponse = await _coursesService.GetAllCoursesAsync(category, searchValue, pageNumber, pageSize);
                if (!string.IsNullOrEmpty(category))
                    courseResponse.Category = category;

                if (courseResponse != null)
                {
                    return Ok(courseResponse);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return NotFound(); // kolla in detta senare?
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCourseAsync(int id)
        {
            var course = await _coursesService.GetOneCourseAsync(id);
            if (course != null)
                return Ok(course);

            return NotFound();
        }
        #endregion

        #region Admin actions
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCourseAsync(CourseDto newCourse)
        {
            if (ModelState.IsValid)
            {
                var result = await _coursesService.CreateCourseAsync(newCourse, newCourse.Category);

                if (result != null)
                    return Ok();

                return Conflict();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourseAsync(UpdateCourseDto newModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _coursesService.UpdateCourseAsync(newModel);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NotFound();
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return BadRequest("Invalid information");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            try
            {
                var result = await _coursesService.DeleteCourseAsync(id);
                if (result == true)
                    return Ok("Course deleted");
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return NotFound("Could not find any course with the given id.");
        }
        #endregion
    }
}
