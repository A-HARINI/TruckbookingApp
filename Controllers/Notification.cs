using Microsoft.AspNetCore.Mvc;
using Truck1.Model;
using static Truck1.TruckbookingDTO;



namespace Truck1;
[ApiController]
[Route("Notifications")]
public class NotificationController : Controller{
     private readonly AppDbContext _Context;
     public NotificationController(AppDbContext Context)
   {
     _Context = Context;
   }
[HttpGet]
    public IActionResult GetNotifications()
    {
        try
        {
            var notifications = _Context.Notifications
                .Select(n => new NotificationDTO
                {
                   
                    Customerid = n.Customerid,
                    Notificationtype = n.Notificationtype,
                    Notificationtext = n.Notificationtext
                })
                .ToList();

            return Ok(new ResponseDTO
            {
                Data = notifications,
                Status =Truck1.Response.sucess,
                Message = "Notifications retrieved successfully"
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
    public IActionResult GetNotificationById(long id)
    {
        try
        {
            var notification = _Context.Notifications
                .FirstOrDefault(n => n.Notificationid == id);

            if (notification == null)
            {
                return NotFound(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Notification not found"
                });
            }

            var notificationDTO = new NotificationDTO
            {
                
                Customerid = notification.Customerid,
                Notificationtype = notification.Notificationtype,
                Notificationtext = notification.Notificationtext
            };

            return Ok(new ResponseDTO
            {
                Data = notificationDTO,
                Status = Truck1.Response.sucess,
                Message = "Notification retrieved successfully"
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
    public IActionResult CreateNotification([FromBody] NotificationDTO notificationDTO)
    {
        try
        {
            if (notificationDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Data = null,
                    Status = Truck1.Response.error,
                    Message = "Invalid notification data"
                });
            }

            var notification = new Notification
            {
                Customerid = notificationDTO.Customerid,
                Notificationtype = notificationDTO.Notificationtype,
                Notificationtext = notificationDTO.Notificationtext
            };

            _Context.Notifications.Add(notification);
            _Context.SaveChanges();

            var createdNotificationDTO = new NotificationDTO
            {
               
                Customerid = notification.Customerid,
                Notificationtype = notification.Notificationtype,
                Notificationtext = notification.Notificationtext
            };

            return Ok(new ResponseDTO
            {
                Data = createdNotificationDTO,
                Status = Truck1.Response.sucess,
                Message = "Notification created successfully"
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
    }}