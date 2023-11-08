using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck1.Model;
using static Truck1.TruckbookingDTO;

namespace Truck1;
#nullable disable


[ApiController]
[Route("OVERALLREPORTS")]
public class OVERALLREPORTSController : Controller
{
    private readonly AppDbContext _Context;

    private readonly IConfiguration configuration;
    private readonly IAuthServices authServices;
    private readonly IServices _services;

    public OVERALLREPORTSController(AppDbContext Context, IConfiguration config, IAuthServices auth, IServices services)
    {
        _Context = Context;

        configuration = config;
        authServices = auth;
        _services = services;

    }
    [HttpGet("monthlywise Customer booking truck details ")]
    public IActionResult GetMonthlyConfirmedBookings([FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            var monthlyBookings = _services.GetMonthlyConfirmedBookings(year, month);


            var response = new ResponseDTO
            {
                Data = monthlyBookings,
                Status = Truck1.Response.sucess,
                Message = $"Monthly bookings for {new DateTime(year, month, 1):MMMM yyyy} retrieved successfully"
            };

            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet("daily wise customer booking report")]
    public IActionResult GetDailyBookings([FromQuery] DateTime date)
    {
        try
        {
            var dailyBookings = _services.GetDailyConfirmedBookings(date);

            if (dailyBookings == null || !dailyBookings.Any())
            {
                var result = new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.sucess,
                    Message = "No bookings found for the specified date."
                };
                return NotFound(result);
            }

            var response = new ResponseDTO
            {
                Data = dailyBookings,
                Status = Truck1.Response.sucess,
                Message = $"Daily bookings retrieved successfully for {date.ToShortDateString()}"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = $"Internal server error: {ex.Message}"
            };
            return StatusCode(500, response);
        }
    }
    [HttpGet("available-truck-facilities")]
    public IActionResult GetAvailableTruckFacilities()
    {
        var response = _services.GetAvailableTruckFacilitiesWithHighLoadCapacity(new List<TruckfacilityDTO>());

        if (response.Status == Truck1.Response.sucess)
        {
            return Ok(response);
        }
        else
        {
            return StatusCode(500, response);
        }
    }
    [HttpGet("confirmedbookedtruckdetails/convenience-requested")]
    public IActionResult GetConfirmedBookedTruckDetailsWithConvenienceRequested()
    {
        var response = _services.GetConfirmedBookedTruckDetailsWithConvenienceRequested();

        if (response.Status == Truck1.Response.sucess)
        {   
            return Ok(response);
        }
        else
        {
            return StatusCode(500, response);
        }
    }
    [HttpGet("feedback-counts")]
    public IActionResult GetFeedbackCounts()
    {
        var feedbackCounts = _services.GetFeedbackCounts();

        var response = new ResponseDTO
        {
            Data = feedbackCounts,
            Status = Truck1.Response.sucess,
            Message = "Feedback counts retrieved successfully"
        };

        return Ok(response);
    }


    [HttpGet("offerdetailswithmonthly")]
    public IActionResult GetOfferDetailsForMonthAndYear(int targetMonth, int targetYear)
    {
        try
        {
            var offerDetailDTOs = _services.GetOfferDetailsForSpecificMonth(targetMonth, targetYear);

            var response = new ResponseDTO
            {
                Data = offerDetailDTOs,
                Status = Truck1.Response.sucess,
                Message = "Offer details retrieved successfully"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = $"Internal server error: {ex.Message}"
            };

            return StatusCode(500, errorResponse);
        }
    }
    [HttpGet("MostUsedModeOfPayment")]
    public IActionResult GetMostUsedModeOfPayment()
    {
        try
        {
            var mostUsedModeOfPayment = _Context.Paymentdetails
              .GroupBy(pd => pd.Modeofpaymentid)
              .OrderByDescending(group => group.Count())
              .Select(group => group.Key)
              .FirstOrDefault();

            if (mostUsedModeOfPayment == null)
            {
                return NotFound(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "No mode of payment found"
                });
            }

            var modeOfPayment = _Context.Modeofpayments
              .Where(mp => mp.Modeofpaymentid == mostUsedModeOfPayment)
              .FirstOrDefault();

            if (modeOfPayment == null)
            {
                return NotFound(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Mode of payment not found"
                });
            }

            var modeOfPaymentDTO = new ModeofpaymentDTO
            {
                Modeofpaymentid = modeOfPayment.Modeofpaymentid,
                Paymentdescription = modeOfPayment.Paymentdescription
            };

            return Ok(new ResponseDTO
            {
                Data = modeOfPaymentDTO,
                Status = Truck1.Response.sucess,
                Message = "Most used mode of payment retrieved successfully"
            });
        }
        catch (Exception ex)
        {
            return Ok(new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }
    [HttpGet("TotalAmountByCostType")]
public IActionResult GetTotalAmountByCostType()
{
    try
    {
        var totalAmounts = new
        {
            LocationbasedcostTotal = _Context.Paymentdetails.Sum(pd => pd.Locationbasedcost),
            FestivaltimecostTotal = _Context.Paymentdetails.Sum(pd => pd.Festivaltimecost),
            TravelwithloadcostTotal = _Context.Paymentdetails.Sum(pd => pd.Travelwithloadcost),
           
        };

        return Ok(new ResponseDTO
        {
            Data = totalAmounts,
            Status = Truck1.Response.sucess,
            Message = "Total amounts by cost type retrieved successfully"
        });
    }
    catch (Exception ex)
    {
        return Ok(new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = $"An error occurred: {ex.Message}"
        });
    }
}
[HttpGet("CustomerDetailsAndMessageTypes")]
    public IActionResult GetCustomerDetailsAndMessageTypes()
    {
        try
        {
            var result = _Context.Notifications
                .GroupBy(n => new { n.Customerid, n.Notificationtype })
                .Select(group => new
                {
                    CustomerId = group.Key.Customerid,
                    MessageType = group.Key.Notificationtype,
                    Count = group.Count()
                })
                .ToList();

            return Ok(new
            {
                Data = result,
                Status = 200,
                Message = "Customer details and message types retrieved successfully."
            });
        }
        catch (Exception ex)
        {
            return Ok(new
            {
                Status = 500,
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }

    [HttpGet("NotificationTypeWithHighestCount")]
    public IActionResult GetNotificationTypeWithHighestCount()
    {
        try
        {
            var result = _Context.Notifications
                .GroupBy(n => n.Notificationtype)
                .Select(group => new
                {
                    NotificationType = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            return Ok(new
            {
                Data = result,
                Status = 200,
                Message = "Notification type with the highest count retrieved successfully."
            });
        }
        catch (Exception ex)
        {
            return Ok(new
            {
                Status = 500,
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }
[HttpGet("most-bookings")]
    public IActionResult GetCustomerWithMostTruckBookings()
    {
        var mostBookedCustomer = _services.FindCustomerWithMostTruckBookings();

        if (mostBookedCustomer == null)
        {
            return NotFound(); 
        }

        return Ok(new ResponseDTO{
            Data = mostBookedCustomer,
            Status = Truck1.Response.sucess,
            Message = "This customer have book more times a truck"
        });
    }
    [HttpGet("most-booked")]
public  IActionResult GetMostBookedTruck()
{
    var mostBookedTruck = _Context.Truckbookings
        .GroupBy(tb => tb.Truckid)
        .AsEnumerable() 
        .OrderByDescending(g => g.Count())
        .FirstOrDefault();

    if (mostBookedTruck == null)
    {
        return NotFound("No truck bookings found.");
    }

    var truckId = mostBookedTruck.Key;
    var truck = _Context.Truckbookings
        .Include(tb => tb.Customer)
        .Where(tb => tb.Truckid == truckId)
        .Select(tb => new
        {
            TruckId = truckId,
            CustomerId = tb.Customerid,
            CustomerName = tb.Customer.Name
        })
        .FirstOrDefault();

    if (truck == null)
    {
        return NotFound("Truck information not found.");
    }

    return Ok(truck);
}
  [HttpGet("damaged-products")]
    public IActionResult GetDamagedProducts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var damagedProducts = _Context.Confirmedbookedtruckdetails
                .Where(c => c.Productdamagecovered == true && c.Bookingdate >= startDate && c.Bookingdate <= endDate)
                .ToList();

            return Ok(damagedProducts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");//2023-09-27
        }
    }
    [HttpGet("total-cost-by-month")]
    public IActionResult GetTotalCostByMonth()
    {
        try
        {
            var totalCostByMonth = _Context.Paymentdetails
                .Where(p => p.Createdat.HasValue)
                .GroupBy(p => new { Year = p.Createdat.Value.Year, Month = p.Createdat.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalCost = g.Sum(p => p.Totalcost)
                })
                .ToList();

            return Ok(totalCostByMonth);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
[HttpGet("queries-count-by-date")]
    public IActionResult GetQueriesCountByDate()
    {
        try
        {
            var queryCountsByDate = _Context.Helpdeskqueries
                .Where(q => q.Querydate.HasValue)
                .GroupBy(q => q.Querydate.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    QueryCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(queryCountsByDate);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
 [HttpGet("available-trucks-on-day")]
    public IActionResult GetAvailableTrucksOnDay(DateTime day)
    {
        try
        {
            var availableTrucks = _Context.Truckfacilities
                .Where(truck => truck.IsAvailable == true)
                .Where(truck => truck.Createdat.HasValue && truck.Createdat.Value.Date == day.Date)
                .ToList();

            return Ok(availableTrucks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
[HttpGet("customers-with-offer-applied")]
    public IActionResult GetCustomersWithOfferApplied()
    {
        try
        {
            var customersWithOffer = _Context.Customeroffers
                .Where(offer => offer.Offerapplied == true)
                .GroupBy(offer => new
                {
                    CustomerId = offer.Customerid,
                    Month = offer.Createdat.HasValue ? offer.Createdat.Value.Month : 0, 
                })
                .Select(group => new
                {
                    CustomerId = group.Key.CustomerId,
                    Month = group.Key.Month,
                    CustomerName = group.First().Customer.Name 
                .ToList()});

            return Ok(customersWithOffer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPost("List of the details")]
    public IActionResult actionresult(){
        var response = _Context.Customers.ToArray();
        return Ok (response);
    }
}
































  


