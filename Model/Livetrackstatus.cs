using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Livetrackstatus
{
    public long Trackingid { get; set; }

    public long? Bookingid { get; set; }

    public string? Currentlocation { get; set; }

    public string? Status { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual Truckbooking? Booking { get; set; }
}
