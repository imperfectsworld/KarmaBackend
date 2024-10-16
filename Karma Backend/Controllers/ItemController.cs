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


    }

}
