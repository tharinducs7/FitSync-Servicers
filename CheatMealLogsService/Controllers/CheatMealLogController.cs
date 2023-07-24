using CheatMealLogsService.Data;
using CheatMealLogsService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CheatMealLogsService.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Cheat Meal log services are running. Status: Online");
        }
    }

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheatMealLogController : ControllerBase
    {
        private readonly CheatMealDbContext _context;

        public CheatMealLogController(CheatMealDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CheatMealLog> Get()
        {
            return _context.CheatMealLogs.ToList();
        }

        // GET api/<CheatMealLogController>/user
        [HttpGet("user")]
        public IEnumerable<CheatMealLog> GetByUserId([FromHeader] string userId)
        {
            return _context.CheatMealLogs.Where(logs => logs.UserId == userId).ToList();
        }

        // GET api/<CheatMealLogController>/5
        [HttpGet("{id}")]
        public ActionResult<CheatMealLog> Get(int id, [FromHeader] string userId)
        {
            try
            {
                var cheatMealLog = _context.CheatMealLogs
                    .FirstOrDefault(log => log.Id == id && log.UserId == userId);

                if (cheatMealLog == null)
                    return NotFound();

                return cheatMealLog;
            }
            catch (Exception ex)
            {
                // Log the exception and return the status code and message from the exception.
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/<CheatMealLogController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CheatMealLog cheatMeal)
        {
            try
            {
                _context.CheatMealLogs.Add(cheatMeal);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = cheatMeal.Id }, cheatMeal);
            }
            catch (Exception ex)
            {
                // Log the exception and return the status code and message from the exception.
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/<CheatMealLogController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CheatMealLog updatedCheatMeal)
        {
            try
            {
                var cheatMealLog = await _context.CheatMealLogs.FindAsync(id);
                if (cheatMealLog == null)
                    return NotFound();

                cheatMealLog.Meal = updatedCheatMeal.Meal;
                cheatMealLog.Note = updatedCheatMeal.Note;
                cheatMealLog.Calories = updatedCheatMeal.Calories;
                cheatMealLog.Qty = updatedCheatMeal.Qty;
                cheatMealLog.RecordDate = updatedCheatMeal.RecordDate;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return the status code and message from the exception.
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/<CheatMealLogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cheatMealLog = await _context.CheatMealLogs.FindAsync(id);
                if (cheatMealLog == null)
                    return NotFound();

                _context.CheatMealLogs.Remove(cheatMealLog);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return the status code and message from the exception.
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
