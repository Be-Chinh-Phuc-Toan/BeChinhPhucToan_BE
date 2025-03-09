using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly DataContext _context;

        public OperationsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Operations>> GetOperations()
        {
            var operations = await _context.Operations.ToListAsync();
            if (operations is null)
                return NotFound(new { message = "Không tìm thấy dữ liệu operations" });
            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Operations>> GetOperationById(int id)
        {
            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
            {
                return NotFound(new { message = $"Operation with ID {id} not found" });
            }
            return Ok(operation);
        }
    }
}
