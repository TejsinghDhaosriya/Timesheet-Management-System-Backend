using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using TimesheetService.DTOs.Request;
using TimesheetService.DTOs.Response;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v2/timesheet")]
    [ApiController]
    public class TimeSheetControllerV2 : ControllerBase
    {
        private ITimeSheetService _timeSheetService;
        public TimeSheetControllerV2(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        [HttpGet]
        public IActionResult GetTimeSheets(Guid? userId, long? organizationId, DateTime? startDate, DateTime? endDate, bool withApproval)
        {
            try
            {
                var timesheets = _timeSheetService.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval);
                return Ok(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success: Timesheets record found. ",
                    Data = timesheets
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the timesheets record. ",
                    Error = ex.Message
                });
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
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Timesheet record found. ",
                        Data = sheet
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: TimeSheet not found",
                    Error = $"Timesheet with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the timesheet record. ",
                    Error = ex.Message
                });
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
                return Ok(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success: Timesheet created successfully. ",
                    Data = createdSheet
                });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Header Values is required. ",
                    Error = ex.Message
                });
            }
            catch (FormatException ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Header values must be an integer value. ",
                    Error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while creating a new Timesheet. ",
                    Error = ex.Message
                });
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
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Timesheet record is deleted sucessfully. "
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Timesheet record is not deleted. ",
                    Error = $"Timesheet with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while deleting a Timesheet. ",
                    Error = ex.Message
                });
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
                   var updatedTimesheet =  _timeSheetService.UpdateTimeSheet(id, timeSheet);
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Project record is updated sucessfully. ",
                        Data = updatedTimesheet
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Timesheet record is not updated. ",
                    Error = $"Timesheet with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while updating a Timesheet. ",
                    Error = ex.Message
                });
            }
        }
    }
}
