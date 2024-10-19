using Karma_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgurController(ImgurService imgurService) : ControllerBase
    {
        private readonly ImgurService _imgurService = imgurService;

        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var images = await _imgurService.GetAllImagesAsync(); 
                return Ok(images); 
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Error retrieving images: {e.Message}");
            }
        }


        [HttpGet("{imageHash}")]
        public async Task<IActionResult> GetImage(string imageHash)
        {
            try
            {
                var imageDetails = await _imgurService.GetImageAsync(imageHash);
                return Ok(imageDetails);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Error retrieving image: {e.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var response = await _imgurService.UploadImageAsync(memoryStream.ToArray(), file.FileName);
                    return Ok(response);
                }
            }

            return BadRequest("No file uploaded.");
        }

        [HttpDelete("{imageHash}")]
        public async Task<IActionResult> DeleteImage(string imageHash)
        {
            try
            {
                var result = await _imgurService.DeleteImageAsync(imageHash);
                return Ok($"Image {imageHash} deleted successfully.");
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Error deleting image: {e.Message}");
            }
        }
    }
}
