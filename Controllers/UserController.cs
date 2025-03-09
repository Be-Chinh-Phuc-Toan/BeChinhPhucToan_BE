using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<User>>> getAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("UserExist")]
        public async Task<ActionResult<User>> getUserExist(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return NotFound(new { message = "Email chưa được đăng ký!" });
            return Ok(user);
        }

        [HttpGet("UserNoneExist")]
        public async Task<ActionResult<User>> getUserNoneExist(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return Ok();
            return BadRequest(new { message = "Email đã được đăng ký!" });
        }

        [HttpPost]
        public async Task<ActionResult<User>> addUser([FromBody] User user)
        {
            var newUser = await _context.Users.FindAsync(user.Email);
            if (newUser is null)
            {
                // Tạo OTP
                var otp = new Random().Next(100000, 999999).ToString();
                user.OtpCode = otp;
                user.OtpExpiration = DateTime.UtcNow.AddMinutes(5);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tạo mới thành công!" });
            }

            return BadRequest(new { message = "Email đã được đăng ký!" });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> getUserById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound(new { message = "Người dùng không tồn tại!" });

            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> updateUser(string id, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound(new { message = "Người dùng không tồn tại!" });

            // Cập nhật thông tin (chỉ cập nhật nếu có dữ liệu mới)
            user.FullName = updatedUser.FullName ?? user.FullName;
            user.Gender = updatedUser.Gender ?? user.Gender;
            user.DateOfBirth = updatedUser.DateOfBirth ?? user.DateOfBirth;
            user.Address = updatedUser.Address ?? user.Address;
            user.UpdatedAt = DateTime.UtcNow; // Cập nhật thời gian sửa đổi

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thành công!" });
        }

    }
}
