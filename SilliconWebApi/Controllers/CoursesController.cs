using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilliconWebApi.Filters;
using System.Diagnostics;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [UseApiKey]
    [ApiController]
    public class CoursesController(CourseService coursesService) : ControllerBase
    {
        public readonly CourseService _coursesService = coursesService;

        #region User actions
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var response = new CourseResult();

            try
            {
                var courseList = await _coursesService.GetAllCoursesAsync();
             
                if (courseList != null)
                {
                    response.Courses = courseList;
                    response.Succeeded = true;
                    return Ok(response);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            response.Courses = null;
            response.Succeeded = false;
            return NotFound(response);
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
                var result = await _coursesService.CreateCourseAsync(newCourse);

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
