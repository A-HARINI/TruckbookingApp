using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Truck1.Model;
using static Truck1.TruckbookingDTO;



namespace Truck1;
[ApiController]
[Route("Document")]
public class DocumentController : Controller{
     private readonly AppDbContext _Context;
     public DocumentController(AppDbContext Context)
   {
     _Context = Context;
   }
   [HttpPost("Image")]
    public async Task<ResponseDTO> AddImage(IFormFile file, int id)
    {


        var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = "./wwwroot/Images/" + fileName;

        var sourceStream = file.OpenReadStream();
        var destinationStream = new FileStream(filePath, FileMode.Create);

        await sourceStream.CopyToAsync(destinationStream);

        var user = await _Context.Documents.AddAsync(new Document{
            Id = id,
            ImagePath= fileName
        

        });

        
        await _Context.SaveChangesAsync();

        return new ResponseDTO
        {
            Data = "Images/" + user,
            Message = "NH Truck Images",
            Status = Truck1.Response.sucess

        };
        
    }

    [HttpGet("Image")]
    public IActionResult GetImage(){
        var result =_Context.Documents.ToList();
        return Ok (result);
    }
    [HttpGet("export-truck-booking-status")]
public IActionResult ExportTruckBookingStatus()
{
  
    using (var package = new ExcelPackage())
    {
       
        var worksheet = package.Workbook.Worksheets.Add("TruckBookingStatus");

    
        worksheet.Cells["A1"].Value = "Booking ID";
        worksheet.Cells["B1"].Value = "Truck Number";
        worksheet.Cells["C1"].Value = "Driver Name";
        worksheet.Cells["D1"].Value = "Pickup Location";
        worksheet.Cells["E1"].Value = "Destination Location";
        worksheet.Cells["F1"].Value = "Status";
        worksheet.Cells["G1"].Value = "Delivery Date";

        
        var data = new[]
        {
            new { BookingID = 1001, TruckNumber = "TRK-101", DriverName = "John Doe", PickupLocation = "Warehouse A", DestinationLocation = "Customer X", Status = "In Transit", DeliveryDate = new DateTime(2023, 9, 15) },
            new { BookingID = 1002, TruckNumber = "TRK-102", DriverName = "Jane Smith", PickupLocation = "Warehouse B", DestinationLocation = "Customer Y", Status = "Delivered", DeliveryDate = new DateTime(2023, 9, 17) },
            new { BookingID = 1003, TruckNumber = "TRK-103", DriverName = "Bob Johnson", PickupLocation = "Warehouse C", DestinationLocation = "Customer Z", Status = "Scheduled", DeliveryDate = new DateTime(2023, 9, 20) },
            new { BookingID = 1004, TruckNumber = "TRK-104", DriverName = "Mary Adams", PickupLocation = "Warehouse D", DestinationLocation = "Customer W", Status = "In Transit", DeliveryDate = new DateTime(2023, 9, 18) }
        };

        // Populate data starting from cell A2 (2nd row, 1st column)
        worksheet.Cells["A2"].LoadFromCollection(data);

        // Save the Excel package to a memory stream
        using (var memoryStream = new MemoryStream())
        {
            package.SaveAs(memoryStream);

            // Set response headers for the Excel file
            Response.Headers.Add("Content-Disposition", "attachment; filename=TruckBookingStatus.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.ContentLength = memoryStream.Length;

            // Write the Excel data to the response stream
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.CopyTo(Response.Body);
        }
    }

    // Return the Excel file as a response
    return new FileStreamResult(Response.Body, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
}

[HttpPost("PDF")]
    public async Task<ResponseDTO> AddPDF(IFormFile file, int id)
    {


        var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = "./wwwroot/Images/" + fileName;

        var sourceStream = file.OpenReadStream();
        var destinationStream = new FileStream(filePath, FileMode.Create);

        await sourceStream.CopyToAsync(destinationStream);

        var user = await _Context.Documents.AddAsync(new Document{
            Id = id,
            PdfPath= fileName
        

        });

        
        await _Context.SaveChangesAsync();

        return new ResponseDTO
        {
            Data = "PDF/" + user,
            Message = "Customer invoice amount",
            Status = Truck1.Response.sucess

        };
        
    }

    [HttpGet("PDF")]
    public IActionResult Getpdf(){
        var result =_Context.Documents.ToList();
        return Ok (result);
    }




}

