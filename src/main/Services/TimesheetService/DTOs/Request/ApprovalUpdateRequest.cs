using TimesheetService.Models;

namespace TimesheetService.DTOs.Request
{
    public class ApprovalUpdateRequest
    {
        public Approval_status? Status { get; set; }
        public string? ReasonForRejection { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }
}
