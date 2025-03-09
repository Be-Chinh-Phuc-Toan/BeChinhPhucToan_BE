using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonTypeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<LessonType>>> getAllLessonTypes()
        {
            var lessonTypes = await _context.LessonTypes
                .Include(lt => lt.Operation)
                .ToListAsync();

            if (lessonTypes is null || !lessonTypes.Any())
                return NotFound(new { message = "Không có dữ liệu!" });
            return Ok(lessonTypes);
        }

        [HttpGet]
        public async Task<ActionResult<List<LessonType>>> GetLessonType(int grade, int operationId)
        {
            var lessonTypes = await _context.LessonTypes
                                    .Include(lt => lt.Operation)
                                    .Where(lt => lt.grade == grade && lt.Operation.id == operationId)
                                    .ToListAsync();

            if (lessonTypes == null || lessonTypes.Count == 0)
                return NotFound(new { message = $"Không tìm thấy dữ liệu cho lớp {grade} và operationId {operationId}!" });

            return Ok(lessonTypes);
        }
        [HttpGet("bygrade/{grade}")]
        public async Task<ActionResult<List<LessonType>>> GetLessonTypeByGrade(int grade)
        {
            var lessonTypes = await _context.LessonTypes
                                    .Include(lt => lt.Operation)
                                    .Where(lt => lt.grade == grade)
                                    .ToListAsync();

            if (lessonTypes == null || lessonTypes.Count == 0)
                return NotFound(new { message = $"Không tìm thấy dữ liệu cho lớp {grade}!" });

            return Ok(lessonTypes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonType>> GetLessonTypeById(int id)
        {
            var lessonType = await _context.LessonTypes
                                 .Include(lt => lt.Operation)
                                 .FirstOrDefaultAsync(lt => lt.id == id);

            if (lessonType == null)
                return NotFound(new { message = $"Không tìm thấy LessonType với ID {id}!" });

            return Ok(lessonType);
        }

    }
}
