using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly DataContext _context;

        public GoalController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Goal>>> getAllGoals()
        {
            var goals = await _context.Goals
                .Include(g => g.Student)
                .ToListAsync();
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Goal>> getGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal is null)
                return NotFound(new { message = "Goal is not found!" });
            return Ok(goal);
        }

        [HttpPost]
        public async Task<ActionResult<Goal>> addGoal([FromBody] Goal goal)
        {
            try
            {
                _context.Goals.Add(goal);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Goal>> updateGoal([FromBody] Goal newInfo)
        //{
        //    try
        //    {
        //        var goal = await _context.Goals.FindAsync(newInfo.id);
        //        if (goal is null)
        //            return NotFound(new { message = "Goal is not found!" });

        //        goal.dateStart = newInfo.dateStart;
        //        goal.dateEnd = newInfo.dateEnd;
        //        goal.numberLesson = newInfo.numberLesson;
        //        goal.typeLesson = newInfo.typeLesson;
        //        goal.badgeID = newInfo.badgeID;

        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Updated successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> deleteGoal(int id)
        //{
        //    var goal = await _context.Goals.FindAsync(id);

        //    if (goal is null)
        //        return NotFound(new { message = "Goal is not found!" });

        //    _context.Goals.Remove(goal);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Deleted successfully!" });
        //}
    }
}
