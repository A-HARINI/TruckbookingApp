using Microsoft.AspNetCore.Mvc;
using Truck1.Model;
using static Truck1.TruckbookingDTO;



namespace Truck1;
[ApiController]
[Route("Helpdesk")]
public class HelpdeskController : Controller{
     private readonly AppDbContext _Context;
     public HelpdeskController(AppDbContext Context)
   {
     _Context = Context;
   }
   [HttpGet]
    public IActionResult GetHelpdeskQueries()
    {
        try
        {
            var queries = _Context.Helpdeskqueries
                .Select(q => new HelpdeskqueryDTO
                {
                    
                    Customerid = q.Customerid,
                    Querytext = q.Querytext
                })
                .ToList();

            return Ok(new ResponseDTO
            {
                Data = queries,
                Status = Truck1.Response.sucess,
                Message = "Helpdesk queries retrieved successfully"
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
    public IActionResult GetHelpdeskQueryById(long id)
    {
        try
        {
            var query = _Context.Helpdeskqueries
                .FirstOrDefault(q => q.Queryid == id);

            if (query == null)
            {
                return NotFound(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Helpdesk query not found"
                });
            }

            var queryDTO = new HelpdeskqueryDTO
            {
                
                Customerid = query.Customerid,
                Querytext = query.Querytext
            };

            return Ok(new ResponseDTO
            {
                Data = queryDTO,
                Status = Truck1.Response.sucess,
                Message = "Helpdesk query retrieved successfully"
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
    public IActionResult CreateHelpdeskQuery([FromBody] HelpdeskqueryDTO queryDTO)
    {
        try
        {
            if (queryDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Invalid helpdesk query data"
                });
            }

            var query = new Helpdeskquery
            {
                Customerid = queryDTO.Customerid,
                Querytext = queryDTO.Querytext
            };

            _Context.Helpdeskqueries.Add(query);
            _Context.SaveChanges();

            var createdQueryDTO = new HelpdeskqueryDTO
            {
                
                Customerid = query.Customerid,
                Querytext = query.Querytext
            };

            return Ok(new ResponseDTO
            {
                Data = createdQueryDTO,
                Status = Truck1.Response.sucess,
                Message = "Helpdesk query created successfully"
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
