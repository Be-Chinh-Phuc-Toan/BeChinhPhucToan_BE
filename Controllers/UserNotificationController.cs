using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        private readonly DataContext _context;

        public UserNotificationController(DataContext context)
        {
            _context = context;
        }

        // GET: /UserNotification
        [HttpGet]
        public async Task<ActionResult<List<UserNotification>>> GetAllNotifications()
        {
            var notifications = await _context.UserNotifications.ToListAsync();
            return Ok(notifications);
        }

        // GET: /UserNotification/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserNotification>> GetNotification(int id)
        {
            var notification = await _context.UserNotifications.FirstOrDefaultAsync(n => n.id == id);

            if (notification is null)
                return NotFound(new { message = "Notification not found!" });

            return Ok(notification);
        }

        // POST: /UserNotification
        [HttpPost]
        public async Task<ActionResult> AddNotification([FromBody] UserNotification notification)
        {
            _context.UserNotifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification created successfully!" });
        }

        // DELETE: /UserNotification/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.UserNotifications.FindAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification not found!" });

            _context.UserNotifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification deleted successfully!" });
        }
    }
}
