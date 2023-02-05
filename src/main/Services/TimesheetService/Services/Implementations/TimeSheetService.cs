using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Repositories.Interfaces;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Services.Implementations
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IApprovalService _approvalService;
        public TimeSheetService(ITimeSheetRepository timeSheetRepository, IApprovalService approvalService)
        {
            _timeSheetRepository = timeSheetRepository;
            _approvalService = approvalService;
        }

        public TimeSheet AddTimeSheet(TimeSheet timeSheet, HeaderDTO headerValues)
        {
            var createdSheet = _timeSheetRepository.AddTimeSheet(timeSheet, headerValues);
            var createdApproval = _approvalService.AddApproval(createdSheet, headerValues);
            return createdSheet;
        }

        public void DeleteTimeSheet(TimeSheet timeSheet)
        {
            _timeSheetRepository.DeleteTimeSheet(timeSheet);
        }

        public TimeSheet? UpdateTimeSheet(long id, TimesheetUpdateRequest timeSheet)
        {
            return _timeSheetRepository.UpdateTimeSheet(id, timeSheet);
        }


        public TimeSheet? GetTimeSheet(long id)
        {
            return _timeSheetRepository.GetTimeSheet(id);
        }

        public List<TimeSheet> GetTimeSheets(long? userId, long? organizationId, DateTime? startDate, DateTime? endDate)
        {
            return _timeSheetRepository.GetTimeSheets(userId, organizationId, startDate, endDate);
        }
    }
}
