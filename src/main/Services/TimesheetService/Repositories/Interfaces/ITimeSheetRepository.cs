using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Repositories.Interfaces
{
    public interface ITimeSheetRepository
    {
        TimeSheet AddTimeSheet(TimeSheet timeSheet);
        List<TimeSheet> GetTimeSheets();
        TimeSheet? GetTimeSheet(long id);
        void DeleteTimeSheet(TimeSheet timeSheet);
        TimeSheet? EditTimeSheet(long id, TimesheetEditInputs timeSheet);
    }
}
