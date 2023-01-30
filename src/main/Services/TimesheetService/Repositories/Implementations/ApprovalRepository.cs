using TimesheetService.DBContext;
using TimesheetService.Models;
using TimesheetService.Repositories.Interfaces;

namespace TimesheetService.Repositories.Implementations
{
    public class ApprovalRepository : IApprovalRepository
    {
        private TimeSheetDbContext _approvalContext;
        public ApprovalRepository(TimeSheetDbContext approvalContext)
        {
            _approvalContext = approvalContext;
        }

        public Approval AddApproval(Approval approval)
        {
            approval.CreatedAt = DateTime.Now;
            approval.ModifiedAt = DateTime.Now;
            _approvalContext.Approvals.Add(approval);
            _approvalContext.SaveChanges();
            return approval;
        }

        public void DeleteApproval(Approval approval)
        {
            _approvalContext.Approvals.Remove(approval);
            _approvalContext.SaveChanges();
        }

        public Approval? EditApproval(Approval approval)
        {
            var currentApproval = _approvalContext.Approvals.Find(approval.Id);
            if (currentApproval != null)
            {
                if (approval.Status != null)
                    currentApproval.Status = approval.Status;
                if (approval.ApprovalDate != null)
                    currentApproval.ApprovalDate = approval.ApprovalDate;
                if(approval.ReasonForRejection != null)
                    currentApproval.ReasonForRejection = approval.ReasonForRejection;

                _approvalContext.Update(currentApproval);
                _approvalContext.SaveChanges();
                return currentApproval;
            }
            return null;
        }

        public Approval? GetApproval(long id)
        {
            var approval = _approvalContext.Approvals.Find(id);
            if (approval != null)
            {
                return approval;
            }
            return null;
        }

        public List<Approval> GetApprovals()
        {
            return _approvalContext.Approvals.ToList();
        }
    }
}
