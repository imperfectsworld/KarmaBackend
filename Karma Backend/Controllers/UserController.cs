using Karma_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        KarmaDbContext dbContext = new KarmaDbContext();

        [HttpPost]
        public IActionResult AddUser([FromBody] User u)
        {
            if (dbContext.Users.Any(x => x.GoogleId == u.GoogleId))
            {
                return NoContent();
            }
            //DONT RESET ID DUE TO GOOGLE

            dbContext.Users.Add(u);
            dbContext.SaveChanges();
            return Created("Not Implemented", u);
        }
    }

}