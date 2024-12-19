using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly DataContext _context;

        public FeedbackController(DataContext context)
        {
            _context = context;
        }

        // Get all feedbacks
        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> GetAllFeedbacks()
        {
            var feedbacks = await _context.Feedbacks.Include(f => f.User).ToListAsync();
            return Ok(feedbacks);
        }

        // Get feedbacks by userPhone
        [HttpGet("user/{userPhone}")]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacksByUser(string userPhone)
        {
            try
            {
                var feedbacks = await _context.Feedbacks
                    .Include(f => f.User)
                    .Where(f => f.userPhone == userPhone)
                    .ToListAsync();

                if (feedbacks == null || feedbacks.Count == 0)
                {
                    return NotFound(new { message = "No feedbacks found for the given user." });
                }

                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while fetching feedbacks for the user.",
                    detail = ex.Message
                });
            }
        }

        // Add new feedback
        [HttpPost]
        public async Task<ActionResult<Feedback>> AddFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest(new { message = "Feedback data is required." });
            }

            try
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFeedbacksByUser), new { userPhone = feedback.userPhone }, feedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while adding the feedback.",
                    detail = ex.Message
                });
            }
        }

        // Delete feedback by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedback(int id)
        {
            try
            {
                var feedback = await _context.Feedbacks.FindAsync(id);
                if (feedback == null)
                {
                    return NotFound(new { message = "Feedback not found." });
                }

                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Feedback deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while deleting the feedback.",
                    detail = ex.Message
                });
            }
        }
    }
}
