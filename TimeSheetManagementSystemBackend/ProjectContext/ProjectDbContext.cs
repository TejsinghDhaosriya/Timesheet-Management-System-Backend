using Microsoft.EntityFrameworkCore;
using TimeSheetManagementSystemBackend.Models;

namespace TimeSheetManagementSystemBackend.ProjectContext
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
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
