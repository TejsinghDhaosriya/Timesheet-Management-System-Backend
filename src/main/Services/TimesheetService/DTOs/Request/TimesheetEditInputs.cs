﻿namespace TimesheetService.DTOs.Request
{
    public class TimesheetEditInputs
    {
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? TotalHours { get; set; }
        public int? OvertimeHours { get; set; }
    }
}
