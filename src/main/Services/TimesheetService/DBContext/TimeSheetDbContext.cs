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
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Project>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Process_Statuses)Enum.Parse(typeof(Process_Statuses), v));
            modelBuilder
              .Entity<Approval>()
              .Property(p => p.Status)
              .HasConversion(
                  v => v.ToString(),
                  v => (Approval_status)Enum.Parse(typeof(Approval_status), v));
        }
    }
}
