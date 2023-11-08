using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Truck1.Model;
using System.Net.Http;
using Microsoft.Extensions.Http;



namespace Truck1;
#nullable disable


[ApiController]
[Route("Livetrackstatus")]
public class LivetrackstatusController : Controller
{
  private readonly AppDbContext _Context;
  public LivetrackstatusController(AppDbContext app){
    _Context = app;
  }
  
[HttpPost("updatelocation")]
public IActionResult UpdateLocation([FromBody] LivetrackstatusDTO locationDTO)
{
    if (locationDTO == null || locationDTO.Bookingid == null)
    {
        return BadRequest("Invalid location data");
    }

  
    var newLocation = new Livetrackstatus
    {
        Bookingid = locationDTO.Bookingid.Value,
        Latitude = locationDTO.Latitude,
        Longitude = locationDTO.Longitude,
        Status = locationDTO.Status,
    
    };

    
    _Context.Livetrackstatuses.Add(newLocation);
    _Context.SaveChanges();

    return Ok (new ResponseDTO{
        Data = newLocation,
        Status = Truck1.Response.sucess,
        Message = "Location data updated successfully"

    });
}
// [HttpGet("apikey")]


//     public IActionResult YourAction()
//     {
//         // Get the API key from the request headers
//         if (HttpContext.Request.Headers.TryGetValue("Authorization", out var apiKey))
//         {
//             // apiKey variable now contains the API key
//             string apiKeyValue = apiKey.ToString();

            
//         }
//         else
//         {
            
//             return Unauthorized("API key is missing");
//         }

    

//         return Ok("API key is valid");
//     }
   [HttpGet("/getip")]
        public IActionResult YourAction()
        {
            // Get the user's IP address
            string ipAddress =HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? 
                  HttpContext.Connection.RemoteIpAddress?.ToString();
            
            // Use the IP address as needed
            // ...

            return Ok($"Client IP Address: {ipAddress}");
        }
    }
[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LocationController(IHttpClientFactory httpClientFactory)
{
    _httpClientFactory = httpClientFactory;
}


    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentLocation()
    {
        try
        {
            string apiKey = "865b16557efd473a03ffe87553a00e23";
            string ipAddress = "106.51.1.63"; 

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                string apiUrl = $"http://api.ipstack.com/{ipAddress}?access_key={apiKey}";

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var locationData = JsonSerializer.Deserialize<LocationData>(json);

                    if (locationData != null)
                    {
                        return Ok(locationData);
                    }
                }

                return BadRequest("Failed to retrieve location data.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    

}








