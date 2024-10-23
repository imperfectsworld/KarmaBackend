using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Karma_Backend.Models;

public partial class Community
{
    public int CommunityId { get; set; }

    public int? ZipCode { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
