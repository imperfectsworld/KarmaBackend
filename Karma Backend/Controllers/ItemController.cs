using Karma_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly KarmaDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public ItemController(KarmaDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        KarmaDbContext dbContext = new KarmaDbContext();

        [HttpGet()]
        public IActionResult GetAll()
        {
            //entity framework is used here to call items from the items table
            List<Item> result = dbContext.Items.Include(p => p.Google).ToList();
            return Ok(result);
        }

        [HttpGet("get-image/{imageHash}")]
        public async Task<IActionResult> GetImage(string imageHash)
        {
            try
            {
                // Call Imgur API to retrieve the image information
                var response = await _httpClient.GetAsync($"https://api.imgur.com/3/image/{imageHash}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);  // Return the image data as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving image: {ex.Message}");
            }
        }



        [HttpPost()]

        public IActionResult AddItem([FromBody] Item newItem)
        {
            newItem.Id = 0;
            dbContext.Items.Add(newItem);

            dbContext.SaveChanges();
            return Created("Not Implemented", newItem);
            
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is missing or empty.");
            }

            try
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                var requestContent = new MultipartFormDataContent();
                requestContent.Add(new ByteArrayContent(fileBytes), "image", file.FileName);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "471792422f94881");

                var response = await _httpClient.PostAsync("https://api.imgur.com/3/image", requestContent);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse(responseBody);
                var imageUrl = responseJson["data"]["link"].ToString();
                //saving 3rd party API data - saves image url
                var item = new Item
                {
                    Pic = imageUrl,
                };
                _dbContext.Items.Add(item);
                await _dbContext.SaveChangesAsync();

                return Ok(new { ImageUrl = imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading image: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            Item result = dbContext.Items.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return NotFound("No matching id");
            }
            else
            {
                dbContext.Items.Remove(result);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }

}
