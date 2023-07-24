using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutActivityService.Data;
using WorkoutActivityService.Models;

namespace WorkoutActivityService.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Workut Activity services are running. Status: Online");
        }
    }

   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutActivityController : ControllerBase
    {
        private readonly WorkoutActivityDbContext _context;

        public WorkoutActivityController(WorkoutActivityDbContext context)
        {
            _context = context;
        }
        // GET: api/<WorkoutActivityController>
        [HttpGet]
        public IEnumerable<WorkoutActivity> Get()
        {
            return _context.WorkoutActivities.ToList();
        }

        // GET api/<WorkoutActivityController>/5
        [HttpGet("{id}")]
        public ActionResult<WorkoutActivity> Get(int id)
        {
            var workoutActivity = _context.WorkoutActivities.Find(id);
            if (workoutActivity == null)
                return NotFound();

            return workoutActivity;
        }

        // POST api/<WorkoutActivityController>
        [HttpPost]
        public async Task<ActionResult<WorkoutActivity>> Post([FromBody] WorkoutActivity workoutActivity)
        {
            _context.WorkoutActivities.Add(workoutActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = workoutActivity.Id }, workoutActivity);
        }

        // PUT api/<WorkoutActivityController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkoutActivity updatedWorkoutActivity)
        {
            var workoutActivity = _context.WorkoutActivities.Find(id);
            if (workoutActivity == null)
                return NotFound();

            workoutActivity.WorkoutType = updatedWorkoutActivity.WorkoutType;
            workoutActivity.DurationInMinutes = updatedWorkoutActivity.DurationInMinutes;
            workoutActivity.CaloriesBurnedPerMinute = updatedWorkoutActivity.CaloriesBurnedPerMinute;
            workoutActivity.DistanceInKm = updatedWorkoutActivity.DistanceInKm;
            workoutActivity.DateTime = updatedWorkoutActivity.DateTime;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<WorkoutActivityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workoutActivity = _context.WorkoutActivities.Find(id);
            if (workoutActivity == null)
                return NotFound();

            _context.WorkoutActivities.Remove(workoutActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
