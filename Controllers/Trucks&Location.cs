using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck1.Model;


namespace Truck1;
[ApiController]
[Route("TRUCKS")]
public class TRUCKSController : Controller{
     private readonly AppDbContext _Context;
     public TRUCKSController(AppDbContext Context)
   {
     _Context = Context;}
[HttpPost("confirmedbook/bulk")]
public IActionResult CreateBulkConfirmedBookedTruck([FromBody] List<ConfirmedbookedtruckdetailDTO> truckDetailDTOs)
{
    if (truckDetailDTOs == null || !truckDetailDTOs.Any())
    {
        return BadRequest("Invalid truck details");
    }

    var truckDetails = new List<Confirmedbookedtruckdetail>();

    foreach (var truckDetailDTO in truckDetailDTOs)
    {
        var truckDetail = new Confirmedbookedtruckdetail
        {
            Customerid = truckDetailDTO.Customerid,
            Truckid = truckDetailDTO.Truckid,

            Productdetails = truckDetailDTO.Productdetails,
            Productlicensenumber = truckDetailDTO.Productlicensenumber,
            Companylicensenumber = truckDetailDTO.Companylicensenumber,
            Bookingpersonidproof = truckDetailDTO.Bookingpersonidproof,
            Conveniencerequested = truckDetailDTO.Conveniencerequested,
            Productdamagecovered = truckDetailDTO.Productdamagecovered,
            Status = truckDetailDTO.Status,
        };

        truckDetails.Add(truckDetail);
    }

    _Context.Confirmedbookedtruckdetails.AddRange(truckDetails);
    _Context.SaveChanges();

    return Ok($"Bulk insert of {truckDetails.Count} truck details completed successfully");
}


[HttpGet("{bookingId}")]
public async Task<IActionResult> GetConfirmedBookedTruck(long bookingId)
{
    try
    {
       
        var truckDetail = await _Context.Confirmedbookedtruckdetails
            .FirstOrDefaultAsync(t => t.Bookingid == bookingId);

      
        if (truckDetail == null)
        {
            return NotFound("Truck detail not found");
        }

       
        var truckDetailDTO = new ConfirmedbookedtruckdetailDTO
        {
           
            Truckid = truckDetail.Truckid,
            Productdetails = truckDetail.Productdetails,
            Productlicensenumber = truckDetail.Productlicensenumber,
            Companylicensenumber = truckDetail.Companylicensenumber,
            Bookingpersonidproof = truckDetail.Bookingpersonidproof,
            Conveniencerequested = truckDetail.Conveniencerequested,
            Productdamagecovered = truckDetail.Productdamagecovered,
            Status = truckDetail.Status,
           
        };

       
        return Ok(truckDetailDTO);
    }
    catch (Exception ex)
    {
        
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}
[HttpDelete("confirmedbook/bulk")]
public IActionResult DeleteBulkConfirmedBookedTruck([FromBody] List<long> bookingIds)
{
    if (bookingIds == null || !bookingIds.Any())
    {
        return BadRequest("Invalid booking IDs");
    }

    var deletedCount = 0;

    foreach (var bookingId in bookingIds)
    {
        var truckDetail = _Context.Confirmedbookedtruckdetails.FirstOrDefault(b => b.Bookingid == bookingId);

        if (truckDetail != null)
        {
            _Context.Confirmedbookedtruckdetails.Remove(truckDetail);
            deletedCount++;
        }
    }

    _Context.SaveChanges();

    return Ok($"Bulk deletion of {deletedCount} truck details completed successfully");
}

[HttpGet("confirmedbook")]
public IActionResult GetConfirmedBookedTrucks()
{
    try
    {
        
        var truckDetails = _Context.Confirmedbookedtruckdetails.ToList();

        
        return Ok(truckDetails);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}


  [HttpPost("TRUCKS fACILITY")]
public async Task<IActionResult> CreateTruckfacilities([FromBody] List<TruckfacilityDTO> truckFacilityDTOs)
{
    try
    {
        if (truckFacilityDTOs == null || !truckFacilityDTOs.Any())
        {
            return BadRequest(new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = "Truck facility data is invalid"
            });
        }

        var newTruckfacilities = new List<Truckfacility>();
        var validationErrors = new List<string>();

        foreach (var truckfacilityDTO in truckFacilityDTOs)
        {
            var errors = ValidateTruckFacilityDTO(truckfacilityDTO);
            validationErrors.AddRange(errors);

            if (errors.Any())
            {
                continue; // Skip adding to newTruckfacilities if validation failed
            }

            var newTruckfacility = new Truckfacility
            {
                 Loadcapacity = decimal.Parse(truckfacilityDTO.Loadcapacity.ToString()), // Parse the validated value
                Acnonac = truckfacilityDTO.Acnonac,
                Fasttrack = truckfacilityDTO.Fasttrack,
                Locationid = truckfacilityDTO.Locationid,
                Tollgateapplicable = truckfacilityDTO.Tollgateapplicable,
                NonAc = truckfacilityDTO.NonAc,
                IsAvailable = truckfacilityDTO.IsAvailable
            };

            newTruckfacilities.Add(newTruckfacility);
        }

        if (validationErrors.Any())
        {
            return BadRequest(new ResponseDTO
            {
                Data = null,
                Status = Truck1.Response.error,
                Message = "Validation errors",
                
            });
        }

        _Context.Truckfacilities.AddRange(newTruckfacilities);
        await _Context.SaveChangesAsync();

        return Ok(new ResponseDTO
        {
            Data = $"Bulk insert of {newTruckfacilities.Count} truck facilities completed successfully",
            Status =Truck1.Response.sucess,
            Message = "Success"
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new ResponseDTO
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = $"Internal server error: {ex.Message}"
        });
    }
}


 private List<string> ValidateTruckFacilityDTO(TruckfacilityDTO truckfacilityDTO)
{
    var errors = new List<string>();

    
    if (truckfacilityDTO.Loadcapacity < 0)
    {
        errors.Add("Load capacity cannot be negative.");
    }

    if (truckfacilityDTO.Loadcapacity > 10000) 
    {
        errors.Add("Load capacity exceeds the maximum limit.");
    }

    

    return errors;
}





    
    [HttpGet("Retrive trucks details")]
    public async Task<IActionResult> GetTruckfacilities()
    {
        try
        {
            
            var truckfacilities = await _Context.Truckfacilities.ToListAsync();

           
            return Ok(truckfacilities);
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("Locations")]
    public async Task<IActionResult> CreateLocation([FromBody] LocationDTO locationDTO)
    {
        try
        {
            if (locationDTO == null)
            {
                return BadRequest("Location data is invalid");
            }

          
            var newLocation = new Location
            {
                Locationtype = locationDTO.Locationtype,
                Districtid = locationDTO.Districtid
            };

           
            _Context.Locations.Add(newLocation);
            await _Context.SaveChangesAsync();

           
            var response = new ResponseDTO 
            {
                Data = newLocation,
                Status = Truck1.Response.sucess,
                Message = "Location created successfully"
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

   
    [HttpGet("Get Location")]
    public async Task<IActionResult> GetLocations()
    {
        try
        {
           
            var locations = await _Context.Locations.ToListAsync();

            // Return a success response with the list of locations
            var response = new ResponseDTO
            {
                Data = locations,
                Status =Truck1.Response.sucess,
                Message = "Locations retrieved successfully"
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
   

     [HttpPost("DISTRICTS")]
    public async Task<IActionResult> CreateDistrict([FromBody] DistrictDTO districtDTO)
    {
        try
        {
            if (districtDTO == null)
            {
                return BadRequest("District data is invalid");
            }

         
            if (string.IsNullOrWhiteSpace(districtDTO.Districtname))
            {
                return BadRequest("District name is required.");
            }

            var newDistrict = new District
            {
                Districtname = districtDTO.Districtname,
                Stateid = districtDTO.Stateid,
             
            };

            
            _Context.Districts.Add(newDistrict);
            await _Context.SaveChangesAsync();

           
            var response = new ResponseDTO
            {
                Data = newDistrict,
                Status = Truck1.Response.sucess,
                Message = "District created successfully"
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

    
    [HttpGet("DistrictDetails")]
    public async Task<IActionResult> GetDistricts()
    {
        try
        {
            
            var districts = await _Context.Districts.ToListAsync();

           
            var response = new ResponseDTO
            {
                Data = districts,
                Status = Truck1.Response.sucess,
                Message = "Districts retrieved successfully"
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

    [HttpPost("CreateState")]
    public async Task<IActionResult> CreateStatesBatch([FromBody] List<StateDTO> stateDTOList)
    {
        try
        {
            if (stateDTOList == null || !stateDTOList.Any())
            {
                return BadRequest("State data list is empty");
            }

            
            foreach (var stateDTO in stateDTOList)
            {
                if (string.IsNullOrWhiteSpace(stateDTO.Statename))
                {
                    return BadRequest("State name is required.");
                }
            }

           
            var newStates = stateDTOList.Select(stateDTO => new State
            {
                Statename = stateDTO.Statename,
                Countryid = stateDTO.Countryid,
                
            }).ToList();

           
            _Context.States.AddRange(newStates);
            await _Context.SaveChangesAsync();

            
            var response = new ResponseDTO
            {
                Data = newStates,
                Status = Truck1.Response.sucess,
                Message = "States created successfully"
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
[HttpGet("GETSTATE")]
public async Task<IActionResult> GetStates()
{
    try
    {
      
        var states = await _Context.States.ToListAsync();

       
        if (states == null || states.Count == 0)
        {
            var result = new ResponseDTO
            {
                Data = null,
                Status =Truck1.Response.error,
                Message = "No states found"
            };
            return NotFound(result);
        }

        
        var response = new ResponseDTO
        {
            Data = states,
            Status = Truck1.Response.sucess,
            Message = "States retrieved successfully"
        };
        return Ok(response);
    }
    catch (Exception ex)
    {
        
        var data= new ResponseDTO
        
        {
            Data = null,
            Status = Truck1.Response.error,
            Message = $"Internal server error: {ex.Message}"
        };
        return StatusCode(500, data);
    }
}
[HttpPost("CreateCountry")]
public async Task<IActionResult> CreateCountry([FromBody] CountryDTO countryDTO)
{
    if (countryDTO == null)
    {
        return BadRequest("Country data is invalid");
    }

    if (string.IsNullOrWhiteSpace(countryDTO.Countryname))
    {
        return BadRequest("Country name is required.");
    }

    var newCountry = new Country
    {
        Countryname = countryDTO.Countryname,
        
    };

    _Context.Countries.Add(newCountry);
    await _Context.SaveChangesAsync();

    var response = new ResponseDTO
    {
        Data = newCountry,
        Status = Truck1.Response.sucess,
        Message = "Country created successfully"
    };
    return Ok(response);
}


   
    [HttpGet("GetCountry")]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            
            var countries = await _Context.Countries.ToListAsync();

          
            var response = new ResponseDTO
            
            {
                Data = countries,
                Status = Truck1.Response.sucess,
                Message = "Countries retrieved successfully"
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

    
}












