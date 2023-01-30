using System.ComponentModel.DataAnnotations;

namespace TimesheetService.Models
{
    public class Project
    {
        [Key]
        public long id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime total_time_spent { get; set; }
        public Process_Statuses status { get; set; }
        public long manager_id { get; set; }
        public long organization_id { get; set; }
        public bool is_active { get; set; }
    }

    public enum Process_Statuses
    {
        pending,
        paused,
        started,
        failed,
        success,
        cancelled
    }
}

