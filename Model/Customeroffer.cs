using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Customeroffer
{
    public long Customerofferid { get; set; }

    public long? Customerid { get; set; }

    public long? Offerid { get; set; }

    public bool Offerapplied { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Offerdetail? Offer { get; set; }
}
