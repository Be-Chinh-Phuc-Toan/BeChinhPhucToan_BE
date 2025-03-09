using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public ExerciseStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetByStudent&Exercise")]
        public async Task<ActionResult<ExerciseStatus>> getByStudentAndExercise(int studentID, int exerciseID)
        {
            var status = await _context.ExerciseStatuses
                .Where(es => es.studentID == studentID && es.exerciseID == exerciseID)
                .ToListAsync();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseStatus>> addExerciseStatus([FromBody] ExerciseStatus status)
        {
            try
            {
                _context.ExerciseStatuses.Add(status);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tạo mới thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi xảy ra!" });
            }            
        }
        [HttpPut("UpdateByStudentAndExercise")]
        public async Task<ActionResult> updateExerciseStatus(int studentID, int exerciseID, [FromBody] ExerciseStatus updatedStatus)
        {
            try
            {
                var existingStatus = await _context.ExerciseStatuses
                    .FirstOrDefaultAsync(es => es.studentID == studentID && es.exerciseID == exerciseID);

                if (existingStatus == null)
                {
                    return NotFound(new { message = "Không tìm thấy trạng thái bài tập!" });
                }

                existingStatus.point = updatedStatus.point;
                existingStatus.isComplete = updatedStatus.isComplete;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi xảy ra!" });
            }
        }
    }
}
