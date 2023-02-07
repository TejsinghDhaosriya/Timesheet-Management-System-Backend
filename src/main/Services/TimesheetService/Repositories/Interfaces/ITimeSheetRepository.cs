using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Repositories.Interfaces
{
    public interface ITimeSheetRepository
    {
        TimeSheet AddTimeSheet(TimeSheet timeSheet, HeaderDTO headerValues);
        List<TimeSheet> GetTimeSheets(long? userId, long? organizationId, DateTime? startDate, DateTime? endDate, bool withApproval);
        TimeSheet? GetTimeSheet(long id);
        void DeleteTimeSheet(TimeSheet timeSheet);
        TimeSheet? UpdateTimeSheet(long id, TimesheetUpdateRequest timeSheet);
    }
}
