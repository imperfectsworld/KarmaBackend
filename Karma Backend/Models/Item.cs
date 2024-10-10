using System;
using System.Collections.Generic;

namespace Karma_Backend.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Pic { get; set; }

    public string? Description { get; set; }

    public string? Condition { get; set; }

    public string? Categories { get; set; }

    public string? GeoCode { get; set; }

    public int? Likes { get; set; }

    public string? GoogleId { get; set; }

    public virtual User? Google { get; set; }
}
