using TimesheetService.Models;

namespace TimesheetService.Services.Interfaces
{
    public interface IApprovalService
    {
        Approval AddApproval(Approval approval);
        List<Approval> GetApprovals();
        Approval? GetApproval(long id);
        void DeleteApproval(Approval approval);
        Approval? EditApproval(Approval approval);
    }
}
