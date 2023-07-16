using CheatMealLogsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheatMealLogsService.Data
{
    public class CheatMealDbContext : DbContext
    {
        public DbSet<CheatMealLog> CheatMealLogs { get; set; }

        public CheatMealDbContext(DbContextOptions<CheatMealDbContext> options) : base(options)
        {
        }
    }
}
