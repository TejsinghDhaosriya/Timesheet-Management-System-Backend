using TimesheetService.DBContext;
using TimesheetService.DTOs.Request;
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

        public Approval? AddApproval(TimeSheet timeSheet, HeaderDTO headerValues)
        {
            var projectId = headerValues.ProjectId;
            var project = _approvalContext.Projects.Find(projectId);
            if (project != null)
            {
                Approval approval = new Approval();
                approval.TimesheetId = timeSheet.Id;
                approval.OrganizationId = headerValues.OrganizationId;
                approval.ManagerId = project.ManagerId;
                approval.Status = 0;
                approval.CreatedAt = DateTime.Now;
                approval.ModifiedAt = DateTime.Now;
                _approvalContext.Approvals.Add(approval);
                _approvalContext.SaveChanges();
                return approval;
            }
            return null;
        }

        public void DeleteApproval(Approval approval)
        {
            _approvalContext.Approvals.Remove(approval);
            _approvalContext.SaveChanges();
        }

        public Approval? UpdateApproval(long id, ApprovalUpdateRequest approval)
        {
            var currentApproval = _approvalContext.Approvals.Find(id);
            if (currentApproval != null)
            {
                if (approval.Status != null)
                    currentApproval.Status = (Approval_status)approval.Status;
                if (approval.ReasonForRejection != null)
                    currentApproval.ReasonForRejection = approval.ReasonForRejection;

                currentApproval.ApprovalDate = DateTime.UtcNow;
                currentApproval.ModifiedAt = DateTime.UtcNow;
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

        public List<Approval>? UpdateApprovals(List<Approval> approvals)
        {
            List<Approval> approvalsList = new List<Approval>();
            foreach (var approval in approvals)
            {
                var currentApproval = _approvalContext.Approvals.Find(approval.Id);
                if (currentApproval != null)
                {
                    currentApproval.Status = approval.Status;
                    currentApproval.ReasonForRejection = approval.ReasonForRejection;
                    currentApproval.ApprovalDate = DateTime.UtcNow;
                    currentApproval.ModifiedAt = DateTime.Now;
                    _approvalContext.Update(currentApproval);
                    _approvalContext.SaveChanges();
                    approvalsList.Add(currentApproval);
                }
                else
                {
                    continue;
                }
            }
            return approvalsList;
          
        }
    }
}
