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

        public IActionResult AddItem([FromBody] Item newItem)
        { 
            newItem.Id = 0;
            dbContext.Items.Add(newItem);

            dbContext.SaveChanges();
            return Created("Not Implemented", newItem);

        }

        //[HttpPost("ItemWithImage")]
        //public async Task<IActionResult> AddItemWithImage([FromForm] ItemCreateDTO itemDto)
        //{
        //    if (itemDto.file != null && itemDto.file.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(itemDto.file.FileName);
        //        var filePath = Path.Combine("YourFileStoragePath", fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await itemDto.file.CopyToAsync(stream);
        //        }
        //        itemDto.item.Pic = filePath;
        //    }
        //    _context.Items.Add(itemDto.item);
        //    await _context.SaveChangesAsync();

        //    return Ok(itemDto.item);
        //}
    }

}
