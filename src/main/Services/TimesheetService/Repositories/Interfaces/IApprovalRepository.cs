using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Repositories.Interfaces
{
    public interface IApprovalRepository
    {
        Approval? AddApproval(TimeSheet timeSheet, HeaderDTO headerValues);
        List<Approval> GetApprovals();
        Approval? GetApproval(long id);
        void DeleteApproval(Approval approval);
        Approval? UpdateApproval(long id, ApprovalUpdateRequest approval);
        List<Approval>? UpdateApprovals(List<Approval> approvals);
    }
}
