using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Services.Interfaces
{
    public interface IApprovalService
    {
        Approval? AddApproval(TimeSheet timeSheet, HeaderDTO headerValues);
        List<Approval> GetApprovals();
        Approval? GetApproval(long id);
        void DeleteApproval(Approval approval);
        Approval? UpdateApproval(long id, ApprovalUpdateRequest approval);
    }
}
