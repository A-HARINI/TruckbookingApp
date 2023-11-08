namespace Truck1;
public partial class ConfirmedbookedtruckdetailDTO
{

   public long? Customerid { get; set; }
    public long? Truckid { get; set; }
  

   

    public string? Productdetails { get; set; }

    public string? Productlicensenumber { get; set; }

    public string? Companylicensenumber { get; set; }

    public string? Bookingpersonidproof { get; set; }

    public bool? Conveniencerequested { get; set; }

    public bool? Productdamagecovered { get; set; }

    public string? Status { get; set; }
}
public partial class CountryDTO
{
    public string Countryname { get; set; } = null!;
}
public partial class CustomerDTO
{
  

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Idproof { get; set; }

    public string? Organizationname { get; set; }

   
}


public partial class CustomerfeedbackDTO
{


    public long? Customerid { get; set; }

    public string? Feedbacktext { get; set; }

    public int? Rating { get; set; }



}

public partial class CustomerofferDTO
{


    public long? Customerid { get; set; }

    public long? Offerid { get; set; }

    public bool Offerapplied { get; set; }
}
public partial class TruckfacilityDTO
{
    

    public decimal Loadcapacity { get; set; }

    public bool Acnonac { get; set; }

    public bool Fasttrack { get; set; }

    public long? Locationid { get; set; }

    public bool Tollgateapplicable { get; set; }

   
    public bool? IsAvailable { get; set; }

    public bool? NonAc { get; set; }}

    public partial class LocationDTO
{

    public string Locationtype { get; set; } = null!;

    public long? Districtid { get; set; }}

    
public partial class DistrictDTO
{
    

    public string Districtname { get; set; } = null!;

    public long? Stateid { get; set; }
}
public partial class StateDTO
{
   

    public string Statename { get; set; } = null!;

    public long? Countryid { get; set; }

}
public class Login{
     public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public partial class OfferdetailDTO
{
    

    public decimal Offerpercentage { get; set; }

    public string Applicablelocationtype { get; set; } = null!;

    public int Minimummonthlybookings { get; set; }
}
public partial class LivetrackstatusDTO
{
    public long? Bookingid { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Status { get; set; }
}
public partial class TruckbookingDTO
{
   

    public long? Customerid { get; set; }

    public long? Truckid { get; set; }

    public long? Pickuplocationid { get; set; }

    public long? Deliverylocationid { get; set; }

    public string? Productdetails { get; set; }

    public string? Productlicensenumber { get; set; }

    public string? Companylicensenumber { get; set; }

    public string? Bookingpersonidproof { get; set; }

    public bool? Conveniencerequested { get; set; }

    public bool? IsProductdamagecovered { get; set; }

    public string? Status { get; set; }
    public partial class PaymentdetailDTO
{
   

    public decimal Loadcapacitybasedcost { get; set; }

    public decimal Truckchoicecost { get; set; }

    public decimal Fasttrackcost { get; set; }

    public decimal Locationbasedcost { get; set; }

    public decimal Festivaltimecost { get; set; }

    public decimal Travelwithloadcost { get; set; }

    public long? Modeofpaymentid { get; set; }

    public decimal Totalcost { get; set; }
    }
    
public partial class PaymentstatusDTO
{
    // public long Paymentid { get; set; }

    public long? Bookingid { get; set; }

    public decimal Paymentamount { get; set; }

    public long? Paymentdetailsid { get; set; }

    public bool? Discountapplied { get; set; }
    }
    public partial class ModeofpaymentDTO
{
    public long Modeofpaymentid { get; set; }

    public string? Paymentdescription { get; set; }

    }
    
public partial class NotificationDTO
{

    public long? Customerid { get; set; }

    public string? Notificationtype { get; set; }

    public string? Notificationtext { get; set; }}

    public partial class HelpdeskqueryDTO
{
   

    public long? Customerid { get; set; }

    public string? Querytext { get; set; }}}
