using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeChinhPhucToan_BE.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        // Get all student
        [HttpGet]
        public async Task<ActionResult<List<Student>>> getAllStudent()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }


        //// Get student by parent email
        [HttpGet("{userId}")]
        public async Task<ActionResult<Student>> getStudent(string userId)
        {
            var student = await _context.Students
                .Where(s => s.userId == userId)
                .ToListAsync();
            return Ok(student);
        }

        //// Post add student 
        [HttpPost]
        public async Task<ActionResult<Student>> addStudent([FromBody] Student student)
        {
            var newStudent = await _context.Students.FirstOrDefaultAsync(s => s.fullName == student.fullName);
            if (newStudent is null)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tạo mới thành công!" });
            }

            return BadRequest(new { message = "Tên của bé đã được đăng ký!" });
        }
        //// Get student by studentId
        [HttpGet("id/{studentId}")]
        public async Task<ActionResult<Student>> getStudentById(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy học sinh!" });
            }
            return Ok(student);
        }

        //// Update student information
        [HttpPut("{studentId}")]
        public async Task<ActionResult> updateStudent(int studentId, [FromBody] Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy học sinh!" });
            }

            student.fullName = updatedStudent.fullName;
            student.image = updatedStudent.image;
            student.dateOfBirth = updatedStudent.dateOfBirth;
            student.grade = updatedStudent.grade;
            student.Setting = updatedStudent.Setting;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật thành công!" });
        }

        //// Delete student by studentId
        [HttpDelete("{studentId}")]
        public async Task<ActionResult> deleteStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Không tìm thấy học sinh!" });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công!" });
        }
    }
}
