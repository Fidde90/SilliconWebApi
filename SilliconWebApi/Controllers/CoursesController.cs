using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(CourseService coursesService) : ControllerBase
    {
        public readonly CourseService _coursesService = coursesService;


        #region Create
        [HttpPost]
        public async Task<IActionResult> CreateCourseAsync()
        {
            return Ok();
        }
        #endregion

        #region Get one/all
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var courseList = await _coursesService.GetAllCoursesAsync();
            if(courseList != null)
                return Ok(courseList);

            return NotFound();
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
    }
}
