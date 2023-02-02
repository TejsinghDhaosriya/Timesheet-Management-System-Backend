using TimesheetService.DBContext;
using TimesheetService.DTOs.Request;
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

        public TimeSheet AddTimeSheet(TimeSheet timeSheet, HeaderDTO headerValues)
        {
            timeSheet.CreatedBy = headerValues.UserID;
            timeSheet.OrganizationId = headerValues.OrganizationId;
            timeSheet.CreatedAt = DateTime.UtcNow;
            timeSheet.ModifiedAt = DateTime.UtcNow;
            _timeSheetContext.TimeSheets.Add(timeSheet);
            _timeSheetContext.SaveChanges();
            return timeSheet;
        }

        public void DeleteTimeSheet(TimeSheet timeSheet)
        {
            _timeSheetContext.TimeSheets.Remove(timeSheet);
            _timeSheetContext.SaveChanges();
        }

        public TimeSheet? UpdateTimeSheet(long id, TimesheetUpdateRequest timeSheet)
        {
            var currentsheet = _timeSheetContext.TimeSheets.Find(id);
            if (currentsheet != null)
            {
                if (timeSheet.Description != null)
                    currentsheet.Description = timeSheet.Description;
                if(timeSheet.Date != null)
                    currentsheet.Date = (DateTime)timeSheet.Date;
                if(timeSheet.TotalHours != null)
                    currentsheet.TotalHours = (int)timeSheet.TotalHours;

                currentsheet.ModifiedAt = DateTime.UtcNow;
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
