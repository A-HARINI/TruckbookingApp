using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Country
{
    public long Countryid { get; set; }

    public string Countryname { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
