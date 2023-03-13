namespace TimesheetService.DTOs.Response
{
    public class TimesheetServiceResponse
    {
        public object? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Warnings { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
       
    }
}
