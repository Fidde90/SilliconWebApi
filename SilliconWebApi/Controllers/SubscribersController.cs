using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SilliconWebApi.Filters;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SilliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class SubscribersController(SubscriberService subscriberService) : ControllerBase
    {
        private readonly SubscriberService _subscriberService = subscriberService;
        private string RegularEx { get; set; } = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreateSubscriptionAsync(SubscriberDto subscriber)
        {
            try
            {
                if (Regex.IsMatch(subscriber.Email, RegularEx))
                {
                    var created = await _subscriberService.CreateSubscriberAsync(subscriber);

                    if (created != null)
                        return Ok(created);

                    return Conflict("A subscriber with the email already exists.");
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }         
            return BadRequest("Enter a vaild Email");
        }
        #endregion

        #region Get All / One
        [HttpGet]
        public async Task<IActionResult> GetAllSubscribersAsync()
        {
            try
            {
                var subscribers = await _subscriberService.GetAllSubscribersAsync();
                if (subscribers != null)
                    return Ok(subscribers);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneSubscriberAsync(int id)
        {
            try
            {
                var subscribers = await _subscriberService.GetOneSubscriberAsync(id);

                if (subscribers != null)
                    return Ok(subscribers);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return NotFound();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneSubscriberAsync(int id, string newEmail)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(newEmail) && Regex.IsMatch(newEmail, RegularEx))
                {
                    var subscribers = await _subscriberService.UpdateSubscriberAsync(id, newEmail);

                    if (subscribers != null)
                        return Ok(subscribers);

                    return NotFound();
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return BadRequest("Enter a vaild email.");
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneSubscriberAsync(int id)
        {
            try
            {
                var result = await _subscriberService.DeleteOneSubscriber(id);
                if (result == true)
                    return Ok("Subscription deleted");
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return NotFound("Could not find a subscription with given id.");
        }
        #endregion
    }
}
