using Microsoft.AspNetCore.Mvc;
using Truck1.Model;

namespace Truck1;
#nullable disable


[ApiController]
[Route("CustomerRegistration")]
public class RegistrationController : Controller
{
  private readonly AppDbContext _Context;
  
  private readonly IConfiguration configuration;
  private readonly IAuthServices authServices;
 
  public RegistrationController(AppDbContext Context,  IConfiguration config, IAuthServices auth)
  {
    _Context = Context;
    
    configuration = config;
    authServices = auth;
    
  }


[HttpPost("Registration")]
  public ActionResult<CustomerDTO> Registration([FromBody] CustomerDTO customer)
  {
    var result = _Context.Customers.FirstOrDefault(H => H.Email == customer.Email);
    if (result != null)
    {
      return Ok("E-Mail already registered successfully");
    }
    string hash = BCrypt.Net.BCrypt.HashPassword(customer.Password);
    var response = _Context.Customers.Add(new Customer
    {


  
    Name = customer.Name,
    Email = customer.Email,
    Contact = customer.Contact,
    Idproof = customer.Idproof,
    Address = customer.Address,
    Organizationname = customer.Organizationname,
    Password = hash

    });
    _Context.SaveChanges();
    return Ok(response.Entity);
  }
  [HttpGet("GetRegistration details")]
  public ActionResult<CustomerDTO> GetRegistration()
  {
    var response = _Context.Customers.ToList();
    return Ok(response);

  }
  [HttpPost("Login")]
  public ActionResult<CustomerDTO> Login([FromBody] Login client)
  {
    var result = _Context.Customers.FirstOrDefault(n => n.Email == client.Email);
    if (result == null)
    {
      return BadRequest("Email mismatch");
    }
    var hash = BCrypt.Net.BCrypt.Verify(client.Password, result.Password);

    if (hash)
    {
      var token = authServices.GetTokenGeneration(result);
      return Ok(new
      {
        token
      });
    }


    return BadRequest(new
    {
      result.Password,
      hash
    });
}}