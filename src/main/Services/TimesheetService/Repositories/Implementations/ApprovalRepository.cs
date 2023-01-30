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
            var currentApproval = _approvalContext.Approvals.Find(approval.id);
            if (currentApproval != null)
            {
                currentApproval.timesheet_id = approval.timesheet_id;
                currentApproval.status = approval.status;
                currentApproval.approval_date = approval.approval_date;
                currentApproval.manager_id = approval.manager_id;
                currentApproval.reason_for_rejection = approval.reason_for_rejection;
                currentApproval.organization_id = approval.organization_id;
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
