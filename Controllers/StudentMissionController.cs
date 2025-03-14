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
    public class StudentMissionController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentMissionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentMission>>> GetStudentMissions()
        {
            var studentMissions = await _context.StudentMissions.Include(sm => sm.Student).Include(sm => sm.Mission).ToListAsync();
            if (studentMissions == null || studentMissions.Count == 0)
                return NotFound(new { message = "Không tìm thấy dữ liệu StudentMissions" });
            return Ok(studentMissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentMission>> GetStudentMissionById(int id)
        {
            var studentMission = await _context.StudentMissions.Include(sm => sm.Student).Include(sm => sm.Mission).FirstOrDefaultAsync(sm => sm.id == id);
            if (studentMission == null)
            {
                return NotFound(new { message = $"StudentMission với ID {id} không tồn tại" });
            }
            return Ok(studentMission);
        }
    }
}
