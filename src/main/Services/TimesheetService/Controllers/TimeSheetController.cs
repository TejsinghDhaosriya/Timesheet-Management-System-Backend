using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        public IActionResult GetTimeSheets(Guid? userId, long? organizationId, DateTime? startDate, DateTime? endDate, bool withApproval)
        {
            try
            {
                return Ok(_timeSheetService.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTimeSheet(long id)
        {
            try
            {
                var sheet = _timeSheetService.GetTimeSheet(id);
                if (sheet != null)
                {
                    return Ok(sheet);
                }
                return NotFound($"TimeSheet with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddTimeSheet(TimeSheet timeSheet)
        {
            try
            {
                HeaderDTO headerValues = new HeaderDTO();
                Request.Headers.TryGetValue("user_id", out StringValues headerValue1);
                Request.Headers.TryGetValue("organization_id", out StringValues headerValue2);
                Request.Headers.TryGetValue("project_id", out StringValues headerValue3);
                headerValues.UserID = Guid.Parse(headerValue1);
                headerValues.OrganizationId = long.Parse(headerValue2);
                headerValues.ProjectId = long.Parse(headerValue3);

                var createdSheet = _timeSheetService.AddTimeSheet(timeSheet, headerValues);
                return Ok(createdSheet);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Header Values is required.");
            }
            catch (FormatException)
            {
                return BadRequest($"Header values must be an integer value.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTimeSheet(long id)
        {
            try
            {
                var sheet = _timeSheetService.GetTimeSheet(id);
                if (sheet != null)
                {
                    _timeSheetService.DeleteTimeSheet(sheet);
                    return Ok("TimeSheet record is deleted sucessfully. ");
                }
                return NotFound($"TimeSheet with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateTimeSheet(long id, TimesheetUpdateRequest timeSheet)
        {
            try
            {
                var currentsheet = _timeSheetService.GetTimeSheet(id);
                if (currentsheet != null)
                {
                    _timeSheetService.UpdateTimeSheet(id, timeSheet);
                    return Ok("TimeSheet record is updated sucessfully. ");
                }
                return NotFound($"TimeSheet with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
