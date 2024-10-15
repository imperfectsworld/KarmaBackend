namespace Karma_Backend.Models
{
    public class ItemCreateDTO
    {
        public Item item{  get; set; }
        public IFormFile file { get; set; }
    }
}
