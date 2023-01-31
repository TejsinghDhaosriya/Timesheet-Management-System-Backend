using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v1/timesheet")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private ITimeSheetService _timeSheetService;
        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        [HttpGet]
        public IActionResult GetTimeSheets()
        {
            return Ok(_timeSheetService.GetTimeSheets());
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTimeSheet(long id)
        {
            var sheet = _timeSheetService.GetTimeSheet(id);
            if (sheet != null)
            {
                return Ok(sheet);
            }
            return NotFound($"TimeSheet with Id {id} was not found.");
        }

        [HttpPost]
        public IActionResult AddTimeSheet(TimeSheet timeSheet)
        {
            var createdSheet = _timeSheetService.AddTimeSheet(timeSheet);
            return Ok(createdSheet);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTimeSheet(long id)
        {
            var sheet = _timeSheetService.GetTimeSheet(id);
            if (sheet != null)
            {
                _timeSheetService.DeleteTimeSheet(sheet);
                return Ok("TimeSheet record is deleted sucessfully. ");
            }
            return NotFound($"TimeSheet with Id {id} was not found.");
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateTimeSheet(long id, TimesheetUpdateRequest timeSheet)
        {
            var currentsheet = _timeSheetService.GetTimeSheet(id);
            if (currentsheet != null)
            {
                _timeSheetService.UpdateTimeSheet(id, timeSheet);
                return Ok("TimeSheet record is updated sucessfully. ");
            }
            return NotFound($"TimeSheet with Id {id} was not found.");
        }
    }
}
