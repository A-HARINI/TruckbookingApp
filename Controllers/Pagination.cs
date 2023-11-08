using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck1.Model;
using static Truck1.TruckbookingDTO;



namespace Truck1;
[ApiController]
[Route("Pagination")]
public class PaginationController : Controller{
     private readonly AppDbContext _Context;
     public PaginationController(AppDbContext Context)
   {
     _Context = Context;
   }
  [HttpGet("Pageination")]
public async Task<IActionResult>GetClient([FromQuery] int page , [FromQuery] int pageSize )
 {

 int skip = (page - 1) * pageSize;


 var categories = await _Context.Confirmedbookedtruckdetails
 .Skip(skip)
.Take(pageSize)
.ToListAsync();

 var totalCount = await _Context.Confirmedbookedtruckdetails.CountAsync();
 return Ok(new{data = categories ,status = 200, message = "success"});
 }}
