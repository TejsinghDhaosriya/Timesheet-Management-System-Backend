using Microsoft.EntityFrameworkCore;
using TimesheetService.Models;

namespace TimesheetService.DBContext
{
    public class TimeSheetDbContext : DbContext
    {
        public TimeSheetDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Project>()
                .Property(p => p.status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Process_Statuses)Enum.Parse(typeof(Process_Statuses), v));
        }
    }
}
