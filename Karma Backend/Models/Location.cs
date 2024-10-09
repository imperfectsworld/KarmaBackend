using System;
using System.Collections.Generic;

namespace Karma_Backend.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
