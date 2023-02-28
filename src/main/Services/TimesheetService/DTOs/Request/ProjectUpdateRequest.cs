using TimesheetService.Models;

namespace TimesheetService.DTOs.Request
{
    public class ProjectUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Process_Statuses? Status { get; set; }
        public string? ManagerId { get; set; }

    }
}
