using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetService.Models
{
    public class Approval
    {
        [Key]
        public long id { get; set; }

        [Column("timesheet_id")]
        public TimeSheet TimesheetId { get; set; }
        public Approval_status status { get; set; }

        [Column("reason_for_rejection")]
        public string? ReasonForRejection { get; set; }

        [Column("approval_date")]
        public DateTime ApprovalDate { get; set; }

        [Column("manager_id")]
        public long ManagerId { get; set; }

        [Column(" organization_id")]
        public long OrganizationId { get; set; }
    }

    public enum Approval_status
    {
        pending,
        success,
        cancelled
    }
}
