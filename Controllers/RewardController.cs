using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        private readonly DataContext _context;

        public RewardController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reward>>> GetAllRewards()
        {
            var rewards = await _context.Rewards.ToListAsync();
            return Ok(rewards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reward>> GetReward(int id)
        {
            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
                return NotFound(new { message = "Reward not found!" });
            return Ok(reward);
        }

        [HttpPost]
        public async Task<ActionResult<Reward>> AddReward([FromBody] Reward reward)
        {
            try
            {
                _context.Rewards.Add(reward);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Reward created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reward>> UpdateReward(int id, [FromBody] Reward newInfo)
        {
            try
            {
                var reward = await _context.Rewards.FindAsync(id);
                if (reward == null)
                    return NotFound(new { message = "Reward not found!" });

                reward.name = newInfo.name;
                reward.image = newInfo.image;
                reward.description = newInfo.description;
                reward.point = newInfo.point;
                reward.isAvailable = newInfo.isAvailable;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Reward updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
                return NotFound(new { message = "Reward not found!" });

            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Reward deleted successfully!" });
        }
    }
}
