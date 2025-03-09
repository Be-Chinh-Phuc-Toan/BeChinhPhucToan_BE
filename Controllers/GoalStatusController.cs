using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoalStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public GoalStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GoalStatus>> getGoalStatus()
        {
            var goalStatus = await _context.GoalStatus
                                   .ToListAsync();
            if (goalStatus is null)
                return NotFound(new { message = $"Không tìm thấy dữ liệu goalstatus" });
            return Ok(goalStatus);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalStatus>>GetGoalStatusById(int id)
        {
            var goalStatus = await _context.GoalStatus.FindAsync(id);
            if(goalStatus == null)
            {
                return NotFound(new { message = $"Goalstatus with ID {id} not found" });
            }
            return Ok(goalStatus);
        }
    }
}
