using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Location
{
    public long Locationid { get; set; }

    public string Locationtype { get; set; } = null!;

    public long? Districtid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<Truckbooking> TruckbookingDeliverylocations { get; set; } = new List<Truckbooking>();

    public virtual ICollection<Truckbooking> TruckbookingPickuplocations { get; set; } = new List<Truckbooking>();

    public virtual ICollection<Truckfacility> Truckfacilities { get; set; } = new List<Truckfacility>();
}
