using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class State
{
    public long Stateid { get; set; }

    public string Statename { get; set; } = null!;

    public long? Countryid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
