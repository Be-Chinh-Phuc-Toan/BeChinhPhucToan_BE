using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotifyUserController : ControllerBase
    {
        private readonly DataContext _context;

        public NotifyUserController(DataContext context)
        {
            _context = context;
        }

        // GET: /NotifyUser
        [HttpGet]
        public async Task<ActionResult<List<NotifyUser>>> GetAllNotifyUsers()
        {
            var notifyUsers = await _context.NotifyUsers
                .Include(nu => nu.UserNotification) // Bao gồm thông tin UserNotification
                .Include(nu => nu.User) // Bao gồm thông tin User
                .ToListAsync();

            return Ok(notifyUsers);
        }

        // GET: /NotifyUser/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<NotifyUser>>> GetNotifyUsersByUserId(string userId)
        {
            var notifyUsers = await _context.NotifyUsers
                .Where(nu => nu.userId == userId)
                .Include(nu => nu.UserNotification)
                .Include(nu => nu.User)
                .ToListAsync();

            if (!notifyUsers.Any())
            {
                return NotFound(new { message = "No notifications found for this user." });
            }

            return Ok(notifyUsers);
        }

        // POST: /NotifyUser
        [HttpPost]
        public async Task<ActionResult<NotifyUser>> AddNotifyUser(NotifyUser notifyUser)
        {
            if (_context.NotifyUsers.Any(nu => nu.userId == notifyUser.userId && nu.notificationID == notifyUser.notificationID))
            {
                return BadRequest(new { message = "Notification already exists for this user." });
            }

            _context.NotifyUsers.Add(notifyUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotifyUsersByUserId), new { userId = notifyUser.userId }, notifyUser);
        }
        // DELETE: /NotifyUser/{userId}/{notificationID}
        [HttpDelete("{userId}/{notificationID}")]
        public async Task<ActionResult> DeleteNotifyUser(string userId, int notificationID)
        {
            var notifyUser = await _context.NotifyUsers
                .FirstOrDefaultAsync(nu => nu.userId == userId && nu.notificationID == notificationID);

            if (notifyUser == null)
            {
                return NotFound(new { message = "Notification not found for this user." });
            }

            _context.NotifyUsers.Remove(notifyUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification deleted successfully." });
        }
    }
}
