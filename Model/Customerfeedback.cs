using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Customerfeedback
{
    public long Feedbackid { get; set; }

    public long? Customerid { get; set; }

    public string? Feedbacktext { get; set; }

    public int? Rating { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }
}
