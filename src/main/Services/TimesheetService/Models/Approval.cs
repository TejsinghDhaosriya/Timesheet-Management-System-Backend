using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetService.Models
{
    public class Approval
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("timesheet_id")]
        [ForeignKey("ID")]
        public virtual long TimesheetId { get; set; }

        [Column("status")]
        public Approval_status Status { get; set; }

        [Column("reason_for_rejection")]
        public string? ReasonForRejection { get; set; }

        [Column("approval_date")]
        public DateTime? ApprovalDate { get; set; }

        [Column("manager_id")]
        public Guid ManagerId { get; set; }

        [Column(" organization_id")]
        public long OrganizationId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("modified_at")]
        public DateTime ModifiedAt { get; set; }
    }

    public enum Approval_status
    {
        pending,
        success,
        cancelled
    }
}
