using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Customer
{
    public long Customerid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Idproof { get; set; }

    public string? Organizationname { get; set; }

    public bool Canbooktruck { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Deletedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Confirmedbookedtruckdetail> Confirmedbookedtruckdetails { get; set; } = new List<Confirmedbookedtruckdetail>();

    public virtual ICollection<Customerfeedback> Customerfeedbacks { get; set; } = new List<Customerfeedback>();

    public virtual ICollection<Customeroffer> Customeroffers { get; set; } = new List<Customeroffer>();

    public virtual ICollection<Helpdeskquery> Helpdeskqueries { get; set; } = new List<Helpdeskquery>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Truckbooking> Truckbookings { get; set; } = new List<Truckbooking>();
}
