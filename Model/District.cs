using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class District
{
    public long Districtid { get; set; }

    public string Districtname { get; set; } = null!;

    public long? Stateid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual State? State { get; set; }
}
