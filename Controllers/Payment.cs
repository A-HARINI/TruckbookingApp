using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck1.Model;
using static Truck1.TruckbookingDTO;


namespace Truck1;
[ApiController]
[Route("Payment")]
public class PaymentController : Controller{
     private readonly AppDbContext _Context;
     public PaymentController(AppDbContext Context)
   {
     _Context = Context;}
[HttpPost("PaymentDetail")]
public IActionResult AddPaymentDetail([FromBody] PaymentdetailDTO paymentDetailDTO)
{
    try
    {
      
        decimal totalCost = paymentDetailDTO.Loadcapacitybasedcost +
                            paymentDetailDTO.Truckchoicecost +
                            paymentDetailDTO.Fasttrackcost +
                            paymentDetailDTO.Locationbasedcost +
                            paymentDetailDTO.Festivaltimecost +
                            paymentDetailDTO.Travelwithloadcost;

        paymentDetailDTO.Totalcost = totalCost;

        
        Paymentdetail paymentDetail = new Paymentdetail
        {
            
            Loadcapacitybasedcost = paymentDetailDTO.Loadcapacitybasedcost,
            Truckchoicecost = paymentDetailDTO.Truckchoicecost,
            Fasttrackcost = paymentDetailDTO.Fasttrackcost,
            Locationbasedcost = paymentDetailDTO.Locationbasedcost,
            Festivaltimecost = paymentDetailDTO.Festivaltimecost,
            Travelwithloadcost = paymentDetailDTO.Travelwithloadcost,
            Modeofpaymentid = paymentDetailDTO.Modeofpaymentid,
            Totalcost = paymentDetailDTO.Totalcost
        };

        
        _Context.Paymentdetails.Add(paymentDetail);

       
        _Context.SaveChanges();

        return Ok(new ResponseDTO
        {
            Data = paymentDetailDTO,
            Status = Truck1.Response.sucess,
            Message = "Payment detail added successfully"
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
[HttpGet("PaymentDetails")]
public IActionResult GetPaymentDetails()
{
    try
    {
        var paymentDetails = _Context.Paymentdetails.ToList();

        return Ok(new ResponseDTO
        {
            Data = paymentDetails,
            Status = Truck1.Response.sucess,
            Message = "Payment details retrieved successfully"
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

[HttpGet("PaymentDetails/{id}")]
public IActionResult GetPaymentDetailById(long id)
{
    try
    {
        var paymentDetail = _Context.Paymentdetails.FirstOrDefault(p => p.Paymentdetailsid == id);

        if (paymentDetail == null)
        {
            return NotFound(new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = "Payment detail not found"
            });
        }

       var paymentDetails = _Context.Paymentdetails.ToList();
        

        return Ok(new ResponseDTO
        {
            Data = paymentDetails,
            Status = Truck1.Response.sucess,
            Message = "Payment detail retrieved successfully"
        });
    }
    catch (Exception ex)
    {
        return Ok(new ResponseDTO
        {
            Data = null,
            Status =Truck1.Response.error,
            Message = $"An error occurred: {ex.Message}"
        });
    }
}
[HttpGet("PaymentStatus")]
    public IActionResult GetPaymentStatus()
    {
        try
        {
            var paymentStatuses = _Context.Paymentstatuses.ToList();

            var paymentStatusDTOs = paymentStatuses.Select(paymentStatus => new PaymentstatusDTO
            {
               
                Bookingid = paymentStatus.Bookingid,
                Paymentamount = paymentStatus.Paymentamount,
                Paymentdetailsid = paymentStatus.Paymentdetailsid,
                Discountapplied = paymentStatus.Discountapplied
            }).ToList();

            return Ok(new ResponseDTO
            {
                Data = paymentStatusDTOs,
                Status = Truck1.Response.sucess,
                Message = "Payment statuses retrieved successfully"
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

    [HttpGet("PaymentStatus/{id}")]
    public IActionResult GetPaymentStatusById(long id)
    {
        try
        {
            var paymentStatus = _Context.Paymentstatuses.FirstOrDefault(p => p.Paymentid == id);

            if (paymentStatus == null)
            {
                return NotFound(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Payment status not found"
                });
            }

            var paymentStatusDTO = new PaymentstatusDTO
            {
                
                Bookingid = paymentStatus.Bookingid,
                Paymentamount = paymentStatus.Paymentamount,
                Paymentdetailsid = paymentStatus.Paymentdetailsid,
                Discountapplied = paymentStatus.Discountapplied
            };

            return Ok(new ResponseDTO
            {
                Data = paymentStatusDTO,
                Status = Truck1.Response.sucess,
                Message = "Payment status retrieved successfully"
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
    [HttpGet]
    public IActionResult GetModeOfPayments()
    {
        try
        {
            var modeOfPayments = _Context.Modeofpayments
                .Select(m => new ModeofpaymentDTO
                {
                    Modeofpaymentid = m.Modeofpaymentid,
                    Paymentdescription = m.Paymentdescription
                })
                .ToList();

            return Ok(new ResponseDTO
            {
                Data = modeOfPayments,
                Status = Truck1.Response.sucess,
                Message = "Mode of payments retrieved successfully"
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

    [HttpGet("{id}")]
    public IActionResult GetModeOfPaymentById(long id)
    {
        try
        {
            var modeOfPayment = _Context.Modeofpayments
                .FirstOrDefault(m => m.Modeofpaymentid == id);

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
                Message = "Mode of payment retrieved successfully"
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

    [HttpPost]
    public IActionResult CreateModeOfPayment([FromBody] ModeofpaymentDTO modeOfPaymentDTO)
    {
        try
        {
            if (modeOfPaymentDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Invalid mode of payment data"
                });
            }

            var modeOfPayment = new Modeofpayment
            {
                Paymentdescription = modeOfPaymentDTO.Paymentdescription
            };

            _Context.Modeofpayments.Add(modeOfPayment);
            _Context.SaveChanges();

            var createdModeOfPaymentDTO = new ModeofpaymentDTO
            {
                Modeofpaymentid = modeOfPayment.Modeofpaymentid,
                Paymentdescription = modeOfPayment.Paymentdescription
            };

            return Ok(new ResponseDTO
            {
                Data = createdModeOfPaymentDTO,
                Status = Truck1.Response.sucess,
                Message = "Mode of payment created successfully"
            });
        }
        catch (Exception ex)
        {
            return Ok(new ResponseDTO
            {
                Data = null,
                Status =Truck1.Response.error,
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }
[HttpPost("PaymentStatus")]
    public IActionResult CreatePaymentStatus([FromBody] PaymentstatusDTO paymentStatusDTO)
    {
        try
        {
            if (paymentStatusDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Invalid payment status data"
                });
            }

            var paymentStatus = new Paymentstatus
            {
                Bookingid = paymentStatusDTO.Bookingid,
                Paymentamount = paymentStatusDTO.Paymentamount,
                Paymentdetailsid = paymentStatusDTO.Paymentdetailsid,
                Discountapplied = paymentStatusDTO.Discountapplied
            };

            _Context.Paymentstatuses.Add(paymentStatus);
            _Context.SaveChanges();

            var createdPaymentStatusDTO = new PaymentstatusDTO
            {
               
                Bookingid = paymentStatus.Bookingid,
                Paymentamount = paymentStatus.Paymentamount,
                Paymentdetailsid = paymentStatus.Paymentdetailsid,
                Discountapplied = paymentStatus.Discountapplied
            };

            return Ok(new ResponseDTO
            {
                Data = createdPaymentStatusDTO,
                Status = Truck1.Response.sucess,
                Message = "Payment status created successfully"
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

     }
  