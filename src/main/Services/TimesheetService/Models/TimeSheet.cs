using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetService.Models
{
    public class TimeSheet
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("total_hours")]
        public int TotalHours { get; set; }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        [Column("organization_id")]
        public long OrganizationId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("modified_at")]
        public DateTime ModifiedAt { get; set; }

        public ICollection<Approval>? approvals { get; set; }
    }
}
