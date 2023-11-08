using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Notification
{
    public long Notificationid { get; set; }

    public long? Customerid { get; set; }

    public string? Notificationtype { get; set; }

    public string? Notificationtext { get; set; }

    public DateTime? Notificationdate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }
}
