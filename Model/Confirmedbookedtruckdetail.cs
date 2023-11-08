using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Confirmedbookedtruckdetail
{
    public long Bookingid { get; set; }

    public long? Customerid { get; set; }

    public long? Truckid { get; set; }

    public DateTime? Bookingdate { get; set; }

    public string? Productdetails { get; set; }

    public string? Productlicensenumber { get; set; }

    public string? Companylicensenumber { get; set; }

    public string? Bookingpersonidproof { get; set; }

    public bool? Conveniencerequested { get; set; }

    public bool? Productdamagecovered { get; set; }

    public string? Status { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Truckfacility? Truck { get; set; }
}
