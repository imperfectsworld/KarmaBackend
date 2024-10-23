using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Karma_Backend.Models;

public partial class User
{
    public string GoogleId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? ProfilePic { get; set; }

    public int? CommunityId { get; set; }

    public string? Email { get; set; }

    [JsonIgnore]
    public virtual Community? Community { get; set; }
    [JsonIgnore]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
