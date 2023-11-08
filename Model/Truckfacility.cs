using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Truckfacility
{
    public long Truckid { get; set; }

    public decimal Loadcapacity { get; set; }

    public bool Acnonac { get; set; }

    public bool Fasttrack { get; set; }

    public long? Locationid { get; set; }

    public bool Tollgateapplicable { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public bool? IsAvailable { get; set; }

    public bool? NonAc { get; set; }

    public virtual ICollection<Confirmedbookedtruckdetail> Confirmedbookedtruckdetails { get; set; } = new List<Confirmedbookedtruckdetail>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<Truckbooking> Truckbookings { get; set; } = new List<Truckbooking>();
}
