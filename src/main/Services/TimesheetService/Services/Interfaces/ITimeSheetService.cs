using TimesheetService.Models;

namespace TimesheetService.Services.Interfaces
{
    public interface ITimeSheetService
    {
        TimeSheet AddTimeSheet(TimeSheet timeSheet);
        List<TimeSheet> GetTimeSheets();
        TimeSheet? GetTimeSheet(long id);
        void DeleteTimeSheet(TimeSheet timeSheet);
        TimeSheet? EditTimeSheet(TimeSheet timeSheet);
    }
}
