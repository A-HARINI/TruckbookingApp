using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Helpdeskquery
{
    public long Queryid { get; set; }

    public long? Customerid { get; set; }

    public string? Querytext { get; set; }

    public DateTime? Querydate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }
}
