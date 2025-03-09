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
            var goals = await _context.Goals.ToListAsync();
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
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetGoalsByUserId(string userId)
        {
            var studentIds = await _context.Students
                                           .Where(s => s.userId == userId)
                                           .Select(s => s.id)
                                           .ToListAsync();

            if (!studentIds.Any())
                return NotFound(new { message = "No Students found for this user!" });

            var goals = await _context.Goals
                .Where(g => studentIds.Contains(g.studentID))
                .Include(g => g.GoalStatus) // Lấy dữ liệu từ bảng GoalStatus
                .Include(g => g.LessonType) 
                .ToListAsync();

            if (!goals.Any())
                return NotFound(new { message = "No goals found for this user's students!" });

            // Trả về dữ liệu có cả StatusName
            var result = goals.Select(g => new
            {
                g.id,
                g.studentID,
                g.lessonTypeID,
                LessonTypeName = g.LessonType != null ? g.LessonType.name : null,
                g.dateStart,
                g.dateEnd,
                g.numberLesson,
                g.badgeID,
                g.reward,
                g.statusID,
                StatusName = g.GoalStatus != null ? g.GoalStatus.name : null,
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Goal>> addGoal([FromBody] Goal goal)
        {
            try
            {
                goal.statusID = 5;
                _context.Goals.Add(goal);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Goal>> updateGoal([FromBody] Goal newInfo)
        {
            try
            {
                var goal = await _context.Goals.FindAsync(newInfo.id);
                if (goal is null)
                    return NotFound(new { message = "Goal is not found!" });

                goal.dateStart = newInfo.dateStart;
                goal.dateEnd = newInfo.dateEnd;
                goal.numberLesson = newInfo.numberLesson;
                goal.LessonType = newInfo.LessonType;
                goal.badgeID = newInfo.badgeID;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);

            if (goal is null)
                return NotFound(new { message = "Goal is not found!" });

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully!" });
        }
        [HttpGet("student/{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetGoalsByStudentId(int id)
        {
            var goals = await _context.Goals
                .Where(g => g.studentID == id)
                .Include(g => g.GoalStatus)
                .Include(g => g.LessonType)
                .ToListAsync();

            if (!goals.Any())
                return NotFound(new { message = "No goals found for this student!" });

            var result = goals.Select(g => new
            {
                g.id,
                g.studentID,
                g.lessonTypeID,
                LessonTypeName = g.LessonType != null ? g.LessonType.name : null,
                g.dateStart,
                g.dateEnd,
                g.numberLesson,
                g.badgeID,
                g.reward,
                g.statusID,
                StatusName = g.GoalStatus != null ? g.GoalStatus.name : null,
            });

            return Ok(result);
        }

    }
}
