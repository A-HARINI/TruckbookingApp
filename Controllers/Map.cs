using Microsoft.AspNetCore.Mvc;

namespace Truck1;
[ApiController]
[Route("Map")]



// public class MapController : ControllerBase
// {
//     [HttpGet("getlocation")]
//     public IActionResult GetLocation(double latitude, double longitude)
//     {
//         // Ensure that the latitude and longitude are valid values.
//         if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
//         {
//             return BadRequest("Invalid latitude or longitude values.");
//         }

//         // Construct the Google Maps URL with the provided latitude and longitude.
//         string googleMapsUrl = $"https://www.google.com/maps?q={latitude},{longitude}";

//         // Redirect the user's browser to the Google Maps URL.
//         return Redirect(googleMapsUrl);
//     }


    public class MapController : ControllerBase
    {
        [HttpGet("getmap")]
        public IActionResult GetMap(double latitude, double longitude)
        {
            // Replace "YOUR_API_KEY" with your actual Google Maps API key.
            string apiKey = "d67286bd3ea294b63d4419a8e72ae7e7-a0310e70-d679-4046-9e35-c2441f33bb92";
            string mapImageUrl = $"https://maps.googleapis.com/maps/api/staticmap?center={latitude},{longitude}&zoom=13&size=400x400&key={apiKey}";

            return Ok(mapImageUrl);
        }
    }





