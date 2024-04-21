using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SilliconWebApi.Filters;
using System.Diagnostics;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [UseApiKey]
    [ApiController]
    public class ContactController(ContactService contactService) : ControllerBase
    {
        private readonly ContactService _contactService = contactService;

        [HttpPost]
        public async Task<IActionResult> CreateMessage(ContactMessageDto dto)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var result = await _contactService.CreateMessageAsync(dto);
                    if (result == true)
                    {
                        return Ok();
                    }
                }

                return BadRequest();
            }
            catch (Exception e) { Debug.WriteLine("Error: {0}", e); }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
