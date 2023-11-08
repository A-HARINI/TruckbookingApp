using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Modeofpayment
{
    public long Modeofpaymentid { get; set; }

    public string? Paymentdescription { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Paymentdetail> Paymentdetails { get; set; } = new List<Paymentdetail>();
}
