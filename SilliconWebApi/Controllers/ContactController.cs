using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using SilliconWebApi.Filters;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [UseApiKey]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMessage(ContactMessageDto dto)
        {


            return Ok();
        }
    }
}
