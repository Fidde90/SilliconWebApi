using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(SubscriberService subscriberService) : ControllerBase
    {
        private readonly SubscriberService _subscriberService = subscriberService;
        private string RegularEx { get; set; } = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(string email)
        {
            if (Regex.IsMatch(email, RegularEx))
            {
                var created = await _subscriberService.CreateSubscriberAsync(email);
                
                if (created != null)
                    return Ok(created);

                return Conflict("A subscriber with the email already exists.");
            }      
            return BadRequest("Enter a vaild Email");
        }
        #endregion

        #region Get All / One
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscribers = await _subscriberService.GetAllSubscribersAsync();

            if(subscribers != null)
                return Ok(subscribers);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var subscribers = await _subscriberService.GetOneSubscriberAsync(id);

            if (subscribers != null)
                return Ok(subscribers);

            return NotFound();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, RegularEx))
            {
                var subscribers = await _subscriberService.UpdateSubscriberAsync(id, email);

                if (subscribers != null)
                    return Ok(subscribers);

                return NotFound();
            }
            return BadRequest("Enter a vaild email.");
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            var result = await _subscriberService.DeleteOneSubscriber(id);
            if(result == true)            
                return Ok("Subscrition deleted");

            return NotFound("Could not find anyone with the given id.");
        }
        #endregion
    }
}
