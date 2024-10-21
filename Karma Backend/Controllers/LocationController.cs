using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Karma_Backend.Services;
using Karma_Backend.Models;
namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await _locationService.GetAll();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving locations: {ex.Message}");
            }
        }

        [HttpPost("save-location")]
        public async Task<IActionResult> SaveLocation([FromBody] AddressDto addressDto)
        {
            if (string.IsNullOrWhiteSpace(addressDto.Address))
            {
                return BadRequest("Address cannot be empty.");
            }

            try
            {
                Location result = await _locationService.SaveLocationAsync(addressDto.Address);
                return Ok(result);
                //return Ok("Location saved successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving location: {ex.Message}");
            }
        }
    }
}
public class AddressDto
{
    public string Address { get; set; } 
}
