using TimesheetService.Models;
using TimesheetService.Repositories.Interfaces;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Services.Implementations
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        public TimeSheetService(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public TimeSheet AddTimeSheet(TimeSheet timeSheet)
        {
            return _timeSheetRepository.AddTimeSheet(timeSheet);
        }

        public void DeleteTimeSheet(TimeSheet timeSheet)
        {
            _timeSheetRepository.DeleteTimeSheet(timeSheet);
        }

        public TimeSheet? EditTimeSheet(TimeSheet timeSheet)
        {
            return _timeSheetRepository.EditTimeSheet(timeSheet);
        }


        public TimeSheet? GetTimeSheet(long id)
        {
            return _timeSheetRepository.GetTimeSheet(id);
        }

        public List<TimeSheet> GetTimeSheets()
        {
            return _timeSheetRepository.GetTimeSheets();
        }
    }
}
