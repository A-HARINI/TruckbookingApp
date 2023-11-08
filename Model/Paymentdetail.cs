using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Paymentdetail
{
    public long Paymentdetailsid { get; set; }

    public decimal Loadcapacitybasedcost { get; set; }

    public decimal Truckchoicecost { get; set; }

    public decimal Fasttrackcost { get; set; }

    public decimal Locationbasedcost { get; set; }

    public decimal Festivaltimecost { get; set; }

    public decimal Travelwithloadcost { get; set; }

    public long? Modeofpaymentid { get; set; }

    public decimal Totalcost { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Modeofpayment? Modeofpayment { get; set; }

    public virtual ICollection<Paymentstatus> Paymentstatuses { get; set; } = new List<Paymentstatus>();
}
