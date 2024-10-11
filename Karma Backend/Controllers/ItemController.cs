using Karma_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        KarmaDbContext dbContext = new KarmaDbContext();

        [HttpGet()]
        public IActionResult GetAll()
        {
            List<Item> result = dbContext.Items.Include(p => p.Google).ToList();
            return Ok(result);
        }


        [HttpPost()]

        public async Task<IActionResult> AddItem([FromForm] Item newItem, IFormFile pic)
        {
            // Handle image upload if file is provided
            if (pic != null && pic.Length > 0)
            {
                // Create a unique file path for the image, you can also use a unique ID or timestamp
                var filePath = Path.Combine("wwwroot/images", pic.FileName);

                // Ensure the folder exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Save the image to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pic.CopyToAsync(stream);
                }

                // Set the image path in the item data
                newItem.Pic = filePath;
            }

            // Save the item data to the database
            newItem.Id = 0;
            dbContext.Items.Add(newItem);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = newItem.Id }, newItem);
        }
    }

}
