using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly SubscriberService _subscriberService;

        public SubscribersController(SubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string email)
        {
            if (ModelState.IsValid)
            {
                var created = await _subscriberService.CreateSubscriberAsync(email);
                if(created != null)
                    return Ok(created);

                return Conflict("A subscriber with the email already exists.");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscribers = await _subscriberService.GetAllSubscribersAsync();

            if(subscribers != null)
                return Ok(subscribers);

            return NoContent();
        }
    }
}
