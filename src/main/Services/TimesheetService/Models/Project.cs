using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetService.Models
{
    public class Project
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Column("status")]
        public Process_Statuses Status { get; set; }

        [Column("manager_id")]
        [Required]
        public long ManagerId { get; set; }

        [Required]
        [Column("organization_id")]
        public long OrganizationId { get; set; }

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("modified_at")]
        public DateTime ModifiedAt { get; set; }
    }

    public enum Process_Statuses
    {
        pending,
        paused,
        started,
        failed,
        success,
        cancelled
    }
}

