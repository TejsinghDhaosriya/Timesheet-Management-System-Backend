namespace TimesheetService.DTOs.Request
{
    public class HeaderDTO
    {
        public Guid UserID { get; set; }
        public long OrganizationId { get; set; }
        public long ProjectId { get; set; }

    }
}
