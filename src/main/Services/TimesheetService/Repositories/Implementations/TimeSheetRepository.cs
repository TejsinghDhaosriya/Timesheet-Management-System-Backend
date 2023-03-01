using Microsoft.EntityFrameworkCore;
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
                if (timeSheet.Date != null)
                    currentsheet.Date = (DateTime)timeSheet.Date;
                if (timeSheet.TotalHours != null)
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

        public List<TimeSheet> GetTimeSheets(Guid? userId, long? organizationId, DateTime? startDate, DateTime? endDate, bool withApproval)
        {
            if (userId != null && organizationId != null && startDate != null && endDate != null && withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.Date >= startDate && t.Date <= endDate && t.CreatedBy == userId && t.OrganizationId == organizationId).Include(t => t.approvals).ToList();
            }
            else
                if (userId != null && organizationId != null && startDate == null && endDate != null && withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.Date <= endDate && t.CreatedBy == userId && t.OrganizationId == organizationId).Include(t => t.approvals).ToList();
            }
            else
                 if (userId != null && organizationId != null && startDate != null && endDate == null && withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.Date >= startDate && t.CreatedBy == userId && t.OrganizationId == organizationId).Include(t => t.approvals).ToList();
            }
            else
                if (startDate != null && endDate != null && withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.Date >= startDate && t.Date <= endDate).Include(t => t.approvals).ToList();
            }
            else
                if (userId != null && organizationId != null && withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.CreatedBy == userId && t.OrganizationId == organizationId).Include(t => t.approvals).ToList();
            }
            else
             if (userId != null && organizationId != null)
            {
                return _timeSheetContext.TimeSheets.Where(t => t.CreatedBy == userId && t.OrganizationId == organizationId).ToList();
            }
            else
                if (withApproval == true)
            {
                return _timeSheetContext.TimeSheets.Include(t => t.approvals).ToList();
            }
            return _timeSheetContext.TimeSheets.ToList();
        }
    }
}
