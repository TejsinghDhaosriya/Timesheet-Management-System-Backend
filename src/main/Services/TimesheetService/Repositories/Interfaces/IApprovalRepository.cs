using TimesheetService.Models;

namespace TimesheetService.Repositories.Interfaces
{
    public interface IApprovalRepository
    {
        Approval AddApproval(Approval approval);
        List<Approval> GetApprovals();
        Approval? GetApproval(long id);
        void DeleteApproval(Approval approval);
        Approval? EditApproval(Approval approval);
    }
}
