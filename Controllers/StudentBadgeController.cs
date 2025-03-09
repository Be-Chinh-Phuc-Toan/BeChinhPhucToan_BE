using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentBadgeController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentBadgeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentBadge>>> GetStudentBadges()
        {
            var studentBadges = await _context.StudentBadges.Include(sb => sb.Student).Include(sb => sb.Badge).ToListAsync();
            if (studentBadges == null || studentBadges.Count == 0)
                return NotFound(new { message = "Không tìm thấy dữ liệu StudentBadges" });
            return Ok(studentBadges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentBadge>> GetStudentBadgeById(int id)
        {
            var studentBadge = await _context.StudentBadges.Include(sb => sb.Student).Include(sb => sb.Badge).FirstOrDefaultAsync(sb => sb.id == id);
            if (studentBadge == null)
            {
                return NotFound(new { message = $"StudentBadge với ID {id} không tồn tại" });
            }
            return Ok(studentBadge);
        }
    }
}
