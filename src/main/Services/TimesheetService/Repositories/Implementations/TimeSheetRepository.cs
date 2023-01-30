using TimesheetService.DBContext;
using TimesheetService.Models;
using TimesheetService.Repositories.Interfaces;

namespace TimesheetService.Repositories.Implementations
{
    public class TimeSheetRepository : ITimeSheetRepository
    {

        private TimeSheetDbContext _timeSheetContext;
        public TimeSheetRepository(TimeSheetDbContext projectContext)
        {
            _timeSheetContext = projectContext;
        }

        public TimeSheet AddTimeSheet(TimeSheet timeSheet)
        {
            _timeSheetContext.TimeSheets.Add(timeSheet);
            _timeSheetContext.SaveChanges();
            return timeSheet;
        }

        public void DeleteTimeSheet(TimeSheet timeSheet)
        {
            _timeSheetContext.TimeSheets.Remove(timeSheet);
            _timeSheetContext.SaveChanges();
        }

        public TimeSheet? EditTimeSheet(TimeSheet timeSheet)
        {
            var currentsheet = _timeSheetContext.TimeSheets.Find(timeSheet.id);
            if (currentsheet != null)
            {
                currentsheet.description = timeSheet.description;
                currentsheet.date = timeSheet.date;
                currentsheet.total_hours = timeSheet.total_hours;
                currentsheet.overtime_hours = timeSheet.overtime_hours;
                _timeSheetContext.Update(currentsheet);
                _timeSheetContext.SaveChanges();
                return currentsheet;
            }
             return null;
        }

        public TimeSheet? GetTimeSheet(long id)
        {
            var sheet = _timeSheetContext.TimeSheets.Find(id);
            if (sheet != null)
            {
                return sheet;
            }
            return null;
        }

        public List<TimeSheet> GetTimeSheets()
        {
            return _timeSheetContext.TimeSheets.ToList();
        }
    }
}
