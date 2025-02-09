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
        //[HttpGet]
        //public async Task<ActionResult<LessonType>> getLessonType (string name, int grade)
        //{
        //    var lessonType = await _context.LessonTypes
        //                           .Where(lt => lt.name == name && lt.grade == grade)
        //                           .SingleOrDefaultAsync();
        //    if (lessonType is null)
        //        return NotFound(new { message = $"Không tìm thấy dữ liệu {name} của lớp {grade}!" });
        //    return Ok(lessonType);
        //}
        [HttpGet]
        public async Task<ActionResult<LessonType>> getLessonType(int grade)
        {
            var lessonType = await _context.LessonTypes
                                   .Where(lt => lt.grade == grade)
                                   .ToListAsync();
            if (lessonType is null)
                return NotFound(new { message = $"của lớp {grade}!" });
            return Ok(lessonType);
        }
    }
}
