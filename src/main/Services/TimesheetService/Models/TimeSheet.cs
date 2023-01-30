using System.ComponentModel.DataAnnotations;

namespace TimesheetService.Models
{
    public class TimeSheet
    {
        [Key]
        public long id { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public TimeOnly start_time { get; set; }
        public TimeOnly end_time { get; set; }
        public int total_hours { get; set; }
        public int overtime_hours { get; set; }
        public long created_by { get; set; }
        public long organization_id { get; set; }
        public ICollection<Approval> approvals { get; set; }
    }
}
