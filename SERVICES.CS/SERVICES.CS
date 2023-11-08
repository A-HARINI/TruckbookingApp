using Microsoft.EntityFrameworkCore;
using Truck1.Model;

namespace Truck1;
    public class Services :IServices{
       private readonly AppDbContext _Context;
       public Services(AppDbContext appDb){
        _Context = appDb;
       }
       public List<Confirmedbookedtruckdetail> GetMonthlyConfirmedBookings(int year, int month)
    {
        
        if (year <= 0 || month < 1 || month > 12)
        {
            throw new ArgumentException("Invalid year or month.");
        }

       
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

      
        var monthlyBookings = _Context.Confirmedbookedtruckdetails
            .Where(b => b.Bookingdate >= startDate && b.Bookingdate <= endDate)
            .ToList();

        return monthlyBookings;
    }

    public List<Confirmedbookedtruckdetail> GetDailyConfirmedBookings(DateTime date)
    {
        
        if (date == default(DateTime))
        {
            throw new ArgumentException("Invalid date.");
        }

      
        var dailyBookings = _Context.Confirmedbookedtruckdetails
           
        .Where(b => b.Bookingdate.HasValue && b.Bookingdate.Value.Date == date.Date)
        .ToList();


        return dailyBookings;
    }

public ResponseDTO GetAvailableTruckFacilitiesWithHighLoadCapacity(List<TruckfacilityDTO> truckfacilities)
{
    // Define the raw SQL query
    string sqlQuery = @"
        SELECT TruckId, LoadCapacity, AcNonAc, LocationId, IsAvailable
        FROM TruckFacility
        WHERE LoadCapacity >= 5000
        AND is_available = true;";

    // Execute the SQL query using FromSqlRaw
    List<Truckfacility> truckFacilities = _Context.Truckfacilities
        .FromSqlRaw(sqlQuery)
        .ToList();

    // Convert the result to a list of DTOs
    List<TruckfacilityDTO> truckFacilityDTOs = truckFacilities
        .Select(truckFacility => new TruckfacilityDTO
        {
            Loadcapacity = truckFacility.Loadcapacity,
            Acnonac = truckFacility.Acnonac,
            Locationid = truckFacility.Locationid,
            IsAvailable = truckFacility.IsAvailable,
            NonAc = truckFacility.NonAc
        })
        .ToList();

    var response = new ResponseDTO
    {
        Data = truckFacilityDTOs,
        Status = Truck1.Response.sucess,
        Message = "Success"
    };

    return response;
}
public ResponseDTO GetConfirmedBookedTruckDetailsWithConvenienceRequested()
{
    
    string sqlQuery = @"
        SELECT BookingId, CustomerId, TruckId, BookingDate, ProductDetails, ProductLicenseNumber, CompanyLicenseNumber, BookingPersonIdProof, Conveniencerequested, ProductDamageCovered, Status
        FROM Confirmedbookedtruckdetail
        WHERE Conveniencerequested = true;";

    List<Confirmedbookedtruckdetail> truckDetails = _Context.Confirmedbookedtruckdetails
        .FromSqlRaw(sqlQuery)
        .ToList();

 
    List<ConfirmedbookedtruckdetailDTO> truckDetailDTOs = truckDetails
        .Select(truckDetail => new ConfirmedbookedtruckdetailDTO
        {
            Customerid = truckDetail.Customerid,
            Truckid = truckDetail.Truckid,
           
            Productdetails = truckDetail.Productdetails,
            Productlicensenumber = truckDetail.Productlicensenumber,
            Companylicensenumber = truckDetail.Companylicensenumber,
            Bookingpersonidproof = truckDetail.Bookingpersonidproof,
            Conveniencerequested = truckDetail.Conveniencerequested,
            Productdamagecovered = truckDetail.Productdamagecovered,
            Status = truckDetail.Status,
        })
        .ToList();

 
    var response = new ResponseDTO
    {
        Data = truckDetailDTOs,
        Status = Truck1.Response.sucess, 
        Message = "Success"
    };

    return response;
}
  public FeedbackCountsDTO GetFeedbackCounts()
    {
        
        var feedback = _Context.Customerfeedbacks;

     
        var positiveFeedbackCount = feedback.Count(f => f.Rating > 4);
        var negativeFeedbackCount = feedback.Count(f => f.Rating <= 4);

        var feedbackCounts = new FeedbackCountsDTO
        {
            PositiveFeedbackCount = positiveFeedbackCount,
            NegativeFeedbackCount = negativeFeedbackCount
        };

        return feedbackCounts;
    }

    public List<string> ValidateOfferDetailDTO(OfferdetailDTO offerDetailDTO)
{
    var errors = new List<string>();

    
    var validOfferPercentages = new List<decimal> { 15m, 10m, 5m };//specifying the percentages as decimal values by adding the m suffix.

    if (!validOfferPercentages.Contains(offerDetailDTO.Offerpercentage))
    {
        errors.Add("Offer percentage must be 15%, 10%, or 5%.");
    }

    if (string.IsNullOrWhiteSpace(offerDetailDTO.Applicablelocationtype))
    {
        errors.Add("Applicable location type is required.");
    }

    if (offerDetailDTO.Minimummonthlybookings < 10)
    {
        errors.Add("Minimum monthly bookings should be 10 or more.");
    }

    return errors;
}

public List<OfferdetailDTO> GetOfferDetailsForSpecificMonth(int targetMonth, int targetYear)
{
    var offerDetailsForSpecificMonth = _Context.Offerdetails
        .Where(o =>
            o.Createdat.HasValue && 
            o.Createdat.Value.Month == targetMonth && 
            o.Createdat.Value.Year == targetYear)
        .ToList();

    
    List<OfferdetailDTO> offerDetailDTOs = offerDetailsForSpecificMonth
        .Select(offerDetail => new OfferdetailDTO
        {
            Offerpercentage = offerDetail.Offerpercentage,
            Applicablelocationtype = offerDetail.Applicablelocationtype,
            Minimummonthlybookings = offerDetail.Minimummonthlybookings
            
        })
        .ToList();

    return offerDetailDTOs;
}
public Customer FindCustomerWithMostTruckBookings()
    {
        var customerWithMostTruckBookings = _Context.Truckbookings
            .GroupBy(tb => tb.Customerid)
            .Select(g => new
            {
                CustomerId = g.Key,
                BookingCount = g.Count()
            })
            .OrderByDescending(x => x.BookingCount)
            .FirstOrDefault();

        if (customerWithMostTruckBookings == null)
        {
         
            return null;
        }

        var mostBookedCustomer = _Context.Customers
            .FirstOrDefault(c => c.Customerid == customerWithMostTruckBookings.CustomerId);

        return mostBookedCustomer;
    }
    public List<Truckbooking> FindTruckWithMostBookings()
    {
        var truckBookings = _Context.Truckbookings
            .GroupBy(tb => tb.Truckid) 
            .Select(group => new
            {
                TruckId = group.Key,
                BookingCount = group.Count()
            })
            .OrderByDescending(x => x.BookingCount) 
            .FirstOrDefault();

        if (truckBookings != null)
        {
            
            var truck = _Context.Truckbookings.FirstOrDefault(t => t.Truckid== truckBookings.TruckId);

            if (truck != null)
            {
                return new List<Truckbooking> { truck };
            }
        }

        return new List<Truckbooking>();
    }
}






























