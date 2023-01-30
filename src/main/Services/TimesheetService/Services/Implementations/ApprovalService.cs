using TimesheetService.Models;
using TimesheetService.Repositories.Interfaces;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Services.Implementations
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _approvalRepository;
        public ApprovalService(IApprovalRepository approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }

        public Approval AddApproval(Approval approval)
        {
            return _approvalRepository.AddApproval(approval);
        }

        public void DeleteApproval(Approval approval)
        {
            _approvalRepository.DeleteApproval(approval);
        }

        public Approval? EditApproval(Approval approval)
        {
            return _approvalRepository.EditApproval(approval);
        }


        public Approval? GetApproval(long id)
        {
            return _approvalRepository.GetApproval(id);
        }

        public List<Approval> GetApprovals()
        {
            return _approvalRepository.GetApprovals();
        }
    }
}
