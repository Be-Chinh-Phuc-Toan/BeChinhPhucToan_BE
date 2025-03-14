using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentRewardController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentRewardController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReward>>> GetStudentRewards()
        {
            var studentRewards = await _context.StudentRewards.Include(sr => sr.Student).Include(sr => sr.Reward).ToListAsync();
            if (studentRewards == null || studentRewards.Count == 0)
                return NotFound(new { message = "Không tìm thấy dữ liệu StudentRewards" });
            return Ok(studentRewards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReward>> GetStudentRewardById(int id)
        {
            var studentReward = await _context.StudentRewards.Include(sr => sr.Student).Include(sr => sr.Reward).FirstOrDefaultAsync(sr => sr.id == id);
            if (studentReward == null)
            {
                return NotFound(new { message = $"StudentReward với ID {id} không tồn tại" });
            }
            return Ok(studentReward);
        }

            [HttpPost]
            public async Task<ActionResult> AddStudentReward([FromBody] StudentReward studentReward)
            {
                try
                {
                    _context.StudentRewards.Add(studentReward);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Created successfully!" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
                }
            }
        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudentReward(int studentId, [FromBody] StudentReward newInfo)
        {
            try
            {
                var studentReward = await _context.StudentRewards.FirstOrDefaultAsync(sr => sr.StudentId == studentId);
                if (studentReward is null)
                    return NotFound(new { message = "StudentReward not found!" });

                studentReward.RewardId = newInfo.RewardId;
                studentReward.quantity = newInfo.quantity;
                studentReward.AchievedDate = newInfo.AchievedDate;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }


        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteStudentReward(int id)
            {
                var studentReward = await _context.StudentRewards.FindAsync(id);
                if (studentReward is null)
                    return NotFound(new { message = "StudentReward not found!" });

                _context.StudentRewards.Remove(studentReward);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Deleted successfully!" });
            }
        }
}
