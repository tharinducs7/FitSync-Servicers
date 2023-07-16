using CheatMealLogsService.Data;
using CheatMealLogsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheatMealLogsService.Controllers
{
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

        // GET api/<CheatMealLogController>/5
        [HttpGet("{id}")]
        public ActionResult<CheatMealLog> Get(int id)
        {
            var cheatMealLog = _context.CheatMealLogs.Find(id);
            if (cheatMealLog == null)
                return NotFound();

            return cheatMealLog;
        }
        // POST api/<CheatMealLogController>
        [HttpPost]
        public async Task<ActionResult<CheatMealLog>> Post([FromBody] CheatMealLog cheatMeal)
        {
            _context.CheatMealLogs.Add(cheatMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cheatMeal.Id }, cheatMeal);
        }
        // PUT api/<CheatMealLogController>/5
        public async Task<IActionResult> Put(int id, [FromBody] CheatMealLog updatedCheatMeal)
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

        // DELETE api/<CheatMealLogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cheatMealLog = await _context.CheatMealLogs.FindAsync(id);
            if (cheatMealLog == null)
                return NotFound();

            _context.CheatMealLogs.Remove(cheatMealLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
