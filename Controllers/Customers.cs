using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Truck1.Model;

namespace Truck1;
#nullable disable


[ApiController]
[Route("Customers")]
public class CustomersController : Controller
{
  private readonly AppDbContext _Context;
  
  private readonly IConfiguration configuration;
  private readonly IAuthServices authServices;
 
  public CustomersController(AppDbContext Context,  IConfiguration config, IAuthServices auth)
  {
    _Context = Context;
    
    configuration = config;
    authServices = auth;
    
    
  }
  [HttpPost("customerfeedback")]

public IActionResult PostCustomerFeedback([FromBody] List<CustomerfeedbackDTO> feedbackDTOs)
{
    if (feedbackDTOs == null || !feedbackDTOs.Any())
    {
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = "Invalid customer feedback data."
        });
    }

    var feedbackEntries = new List<Customerfeedback>();

    foreach (var feedbackDTO in feedbackDTOs)
    {
        var feedbackEntry = new Customerfeedback
        {
            Customerid = feedbackDTO.Customerid,
            Feedbacktext = feedbackDTO.Feedbacktext,
            Rating = feedbackDTO.Rating
        };

        feedbackEntries.Add(feedbackEntry);
    }

    _Context.Customerfeedbacks.AddRange(feedbackEntries);
    _Context.SaveChanges();

    return Ok(new ResponseDTO
    {
        Data = $"Successfully added {feedbackEntries.Count} feedback entries.",
        Status = Truck1.Response.sucess,
        Message = "Success"
    });
}
 [HttpGet("Retrive Customer Feedback")]
        public IActionResult Get()
        {
            var customerFeedbacks = _Context.Customerfeedbacks
                .Select(cf => new CustomerfeedbackDTO
                {
                    Customerid = cf.Customerid,
                    Feedbacktext = cf.Feedbacktext,
                    Rating = cf.Rating
                })
                .ToList();

            return Ok (new ResponseDTO{
               Data = customerFeedbacks,
               Status = Truck1.Response.sucess,
               Message ="customers feedbacks "
        });


  }
  
 [HttpPut("customerfeedback{id}")]
        public IActionResult Put(long id, [FromBody] CustomerfeedbackDTO customerFeedbackDTO)
        {
            var existingFeedback = _Context.Customerfeedbacks.Find(id);

            if (existingFeedback == null)
            {
                return NotFound($"Customer feedback with ID {id} not found");
            }

            existingFeedback.Customerid = customerFeedbackDTO.Customerid;
            existingFeedback.Feedbacktext = customerFeedbackDTO.Feedbacktext;
            existingFeedback.Rating = customerFeedbackDTO.Rating;

            _Context.SaveChanges();

            return Ok($"Customer feedback with ID {id} updated successfully");
        }

        [HttpDelete("customerfeedback{id}")]
        public IActionResult Delete(long id)
        {
            var customerFeedback = _Context.Customerfeedbacks.Find(id);

            if (customerFeedback == null)
            {
                return NotFound($"Customer feedback with ID {id} not found");
            }

            _Context.Customerfeedbacks.Remove(customerFeedback);
            _Context.SaveChanges();

            return Ok($"Customer feedback with ID {id} deleted successfully");
        }

[HttpPost("offerdetails")]
public IActionResult CreateOfferDetails([FromBody] List<OfferdetailDTO> offerDetailDTOs)
{
    if (offerDetailDTOs == null || !offerDetailDTOs.Any())
    {
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = "Invalid offer details"
        });
    }

    var offerDetails = new List<Offerdetail>();

    foreach (var offerDetailDTO in offerDetailDTOs)
    {
     
        
        var offerDetail = new Offerdetail
        {
            Offerpercentage = offerDetailDTO.Offerpercentage,
            Applicablelocationtype = offerDetailDTO.Applicablelocationtype,
            Minimummonthlybookings = offerDetailDTO.Minimummonthlybookings
        };

        offerDetails.Add(offerDetail);
    }

    if (!offerDetails.Any())
    {
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = "All offer details are invalid"
        });
    }

  
    _Context.Offerdetails.AddRange(offerDetails);
    _Context.SaveChanges();

    return Ok(new ResponseDTO
    {
        Data = $"Bulk insert of {offerDetails.Count} offer details completed successfully",
        Status = Truck1.Response.sucess,
        Message = "Success"
    });
}




[HttpGet("GETofferdetails")]
public IActionResult GetOfferDetails()
{
    
    var offerDetails = _Context.Offerdetails.ToList(); 

    
    var offerDetailDTOs = offerDetails
        .Select(offerDetail => new OfferdetailDTO
        {
            Offerpercentage = offerDetail.Offerpercentage,
            Applicablelocationtype = offerDetail.Applicablelocationtype,
            Minimummonthlybookings = offerDetail.Minimummonthlybookings
        })
        .ToList();

   
    var response = new ResponseDTO
    {
        Data = offerDetailDTOs,
        Status = Truck1.Response.sucess,
        Message = "Offer details retrieved successfully"
    };

    return Ok(response);
}
[HttpPost("customeroffers")]
public IActionResult CreateCustomerOffers([FromBody] List<CustomerofferDTO> customerOfferDTOs)
{
    if (customerOfferDTOs == null || !customerOfferDTOs.Any())
    {
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = "Invalid customer offer details"
        });
    }

    var customerOffers = new List<Customeroffer>();

    foreach (var customerOfferDTO in customerOfferDTOs)
    {
        var customerOffer = new Customeroffer
        {
            Customerid = customerOfferDTO.Customerid,
            Offerid = customerOfferDTO.Offerid,
            Offerapplied = customerOfferDTO.Offerapplied
        };

        customerOffers.Add(customerOffer);
    }

    _Context.Customeroffers.AddRange(customerOffers);
    _Context.SaveChanges();

    return Ok(new ResponseDTO
    {
        Data = $"Bulk insert of {customerOffers.Count} customer offers completed successfully",
        Status = Truck1.Response.sucess,
        Message = "Success"
    });
}
[HttpGet("customeroffers")]
public IActionResult GetCustomerOffers()

    {
        
        var customerOffers = _Context.Customeroffers.ToList();
return Ok(new ResponseDTO
    {
        Data = customerOffers,
        Status = Truck1.Response.sucess,
        Message = "Success"
    });
}
[HttpPost("truckbookings")]
public IActionResult CreateTruckBooking([FromBody] TruckbookingDTO truckBookingDTO)
{
    try
    {
       
        var truckBooking = new Truckbooking
        {
            Customerid = truckBookingDTO.Customerid,
            Truckid = truckBookingDTO.Truckid,
            Pickuplocationid = truckBookingDTO.Pickuplocationid,
            Deliverylocationid = truckBookingDTO.Deliverylocationid,
            Productdetails = truckBookingDTO.Productdetails,
            Productlicensenumber = truckBookingDTO.Productlicensenumber,
            Companylicensenumber = truckBookingDTO.Companylicensenumber,
            Bookingpersonidproof = truckBookingDTO.Bookingpersonidproof,
            Conveniencerequested = truckBookingDTO.Conveniencerequested,
            IsProductdamagecovered = truckBookingDTO.IsProductdamagecovered,
            Status = truckBookingDTO.Status
        };

       
        _Context.Truckbookings.Add(truckBooking);
        _Context.SaveChanges();

   
        return Ok(new ResponseDTO
        {
            Data = truckBooking,
            Status = Truck1.Response.sucess,
            Message = "Truck booking created successfully"
        });
    }
    catch (Exception ex)
    {
        
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = $"Error creating truck booking: {ex.Message}"
        });
    }
}
[HttpGet("gETtruckbookings")]
public IActionResult GetTruckBookings()
{
    try
    {
       
        var truckBookings = _Context.Truckbookings.ToList();

     
        var truckBookingDTOs = truckBookings.Select(truckBooking => new TruckbookingDTO
        {
            Customerid = truckBooking.Customerid,
            Truckid = truckBooking.Truckid,
            Pickuplocationid = truckBooking.Pickuplocationid,
            Deliverylocationid = truckBooking.Deliverylocationid,
            Productdetails = truckBooking.Productdetails,
            Productlicensenumber = truckBooking.Productlicensenumber,
            Companylicensenumber = truckBooking.Companylicensenumber,
            Bookingpersonidproof = truckBooking.Bookingpersonidproof,
            Conveniencerequested = truckBooking.Conveniencerequested,
            IsProductdamagecovered = truckBooking.IsProductdamagecovered,
            Status = truckBooking.Status
        }).ToList();

       
        return Ok(new ResponseDTO
        {
            Data = truckBookingDTOs,
            Status = Truck1.Response.sucess,
            Message = "Truck bookings retrieved successfully"
        });
    }
    catch (Exception ex)
    {
        
        return BadRequest(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = $"Error retrieving truck bookings: {ex.Message}"
        });
    }
}



} 



    

  
