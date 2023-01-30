using TimesheetService.Models;

namespace TimesheetService.DTOs.Request
{
    public class ProjectEditInputs
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Process_Statuses? Status { get; set; }
        public long? ManagerId { get; set; }

    }
}
