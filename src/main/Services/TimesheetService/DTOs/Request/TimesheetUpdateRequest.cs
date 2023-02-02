namespace TimesheetService.DTOs.Request
{
    public class TimesheetUpdateRequest
    {
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? TotalHours { get; set; }
    }
}
