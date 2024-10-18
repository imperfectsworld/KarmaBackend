using Karma_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgurController : ControllerBase
    {
        [HttpGet("client-id")]
        public IActionResult GetImgurClientId()
        {
            string clientId = Secret.ImgurClientId;
            if (!string.IsNullOrEmpty(clientId))
            {
                return Ok(new { clientId });
            }
            return BadRequest("IMGUR Client ID is missing");
        }
    }
}
