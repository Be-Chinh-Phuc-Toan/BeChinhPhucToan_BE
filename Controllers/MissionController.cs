using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly DataContext _context;

        public MissionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mission>>> GetAllMissions()
        {
            var missions = await _context.Missions.ToListAsync();
            return Ok(missions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
                return NotFound(new { message = "Mission not found!" });
            return Ok(mission);
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> AddMission([FromBody] Mission mission)
        {
            try
            {
                _context.Missions.Add(mission);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Mission created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Mission>> UpdateMission(int id, [FromBody] Mission newInfo)
        {
            try
            {
                var mission = await _context.Missions.FindAsync(id);
                if (mission == null)
                    return NotFound(new { message = "Mission not found!" });

                mission.ExerciseId = newInfo.ExerciseId;
                mission.RewardId = newInfo.RewardId;
                mission.DateStart = newInfo.DateStart;
                mission.DateEnd = newInfo.DateEnd;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Mission updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
                return NotFound(new { message = "Mission not found!" });

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Mission deleted successfully!" });
        }
    }
}
