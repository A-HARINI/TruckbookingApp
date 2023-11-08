using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Offerdetail
{
    public long Offerid { get; set; }

    public decimal Offerpercentage { get; set; }

    public string Applicablelocationtype { get; set; } = null!;

    public int Minimummonthlybookings { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Customeroffer> Customeroffers { get; set; } = new List<Customeroffer>();
}
