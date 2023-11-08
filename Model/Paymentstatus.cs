using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Paymentstatus
{
    public long Paymentid { get; set; }

    public long? Bookingid { get; set; }

    public decimal Paymentamount { get; set; }

    public long? Paymentdetailsid { get; set; }

    public bool? Discountapplied { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Truckbooking? Booking { get; set; }

    public virtual Paymentdetail? Paymentdetails { get; set; }
}
