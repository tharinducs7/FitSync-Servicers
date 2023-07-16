using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutActivityService.Models;

namespace WorkoutActivityService.Data
{
    public class WorkoutActivityDbContext : DbContext
    {
        public DbSet<WorkoutActivity> WorkoutActivities { get; set; }

        public WorkoutActivityDbContext(DbContextOptions<WorkoutActivityDbContext> options) : base(options)
        {
        }
    }
}
