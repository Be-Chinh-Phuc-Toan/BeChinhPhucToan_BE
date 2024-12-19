﻿using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotifyParentController : ControllerBase
    {
        private readonly DataContext _context;

        public NotifyParentController(DataContext context)
        {
            _context = context;
        }

        // GET: /NotifyParent
        [HttpGet]
        public async Task<ActionResult<List<NotifyParent>>> getAllNotifyParents()
        {
            var notifyParents = await _context.NotifyParents
                .Include(np => np.Parent)
                .Include(np => np.ParentNotification)
                .ToListAsync();

            return Ok(notifyParents);
        }

        // POST: /NotifyParent
        [HttpPost]
        public async Task<ActionResult> addNotifyParent([FromBody] NotifyParent notifyParent)
        {
            // Kiểm tra khóa ngoại
            var parentExists = await _context.Parents.AnyAsync(p => p.email == notifyParent.parentEmail);
            var notificationExists = await _context.ParentNotifications.AnyAsync(n => n.id == notifyParent.notificationID);

            if (!parentExists || !notificationExists)
                return BadRequest(new { message = "Parent or Notification does not exist!" });

            _context.NotifyParents.Add(notifyParent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "NotifyParent created successfully!" });
        }

        // DELETE: /NotifyParent/{parentEmail}/{notificationID}
        [HttpDelete("{parentEmail}/{notificationID}")]
        public async Task<IActionResult> deleteNotifyParent(string parentEmail, int notificationID)
        {
            var notifyParent = await _context.NotifyParents
                .FirstOrDefaultAsync(np => np.parentEmail == parentEmail && np.notificationID == notificationID);

            if (notifyParent == null)
                return NotFound(new { message = "NotifyParent not found!" });

            _context.NotifyParents.Remove(notifyParent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "NotifyParent deleted successfully!" });
        }
    }
}