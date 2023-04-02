using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TimesheetService.DTOs.Request;
using TimesheetService.DTOs.Response;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v2/timesheet/approval")]
    [ApiController]
    public class ApprovalControllerV2 : ControllerBase
    {
        private IApprovalService _approvalService;
        public ApprovalControllerV2(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }
        [HttpGet]
        public IActionResult GetApprovals()
        {
            try
            {
                var approvals = _approvalService.GetApprovals();
                return Ok(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success: Approvals record found. ",
                    Data = approvals
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the approvals record. ",
                    Error = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetApproval(long id)
        {
            try
            {
                var approval = _approvalService.GetApproval(id);
                if (approval != null)
                {
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Approval record found. ",
                        Data = approval
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Approval not found. ",
                    Error = $"Approval with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the approval record. ",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteApproval(long id)
        {
            try
            {
                var approval = _approvalService.GetApproval(id);
                if (approval != null)
                {
                    _approvalService.DeleteApproval(approval);
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Approval record is deleted sucessfully. "
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Approval record is not deleted. ",
                    Error = $"Approval with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while deleting a Approval. ",
                    Error = ex.Message
                });
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateApproval(long id, ApprovalUpdateRequest approval)
        {
            try
            {
                var currentApproval = _approvalService.GetApproval(id);
                if (currentApproval != null)
                {
                   var updatedApproval =  _approvalService.UpdateApproval(id, approval);
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Approval record is updated sucessfully. ",
                        Data = updatedApproval
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Approval record is not updated. ",
                    Error = $"Approval with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while updating a Approval. ",
                    Error = ex.Message
                });
            }
        }

        [HttpPatch]
        [Route("/api/v2/timesheet/approvals")]
        public IActionResult UpdateApprovals(List<Approval> approvals)
        {
            try
            {
                var updatedApprovals = _approvalService.UpdateApprovals(approvals);
                return Ok(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success: Approvals record is updated sucessfully. ",
                    Data = updatedApprovals
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while updating Approvals. ",
                    Error = ex.Message
                });
            }
        }
    }
}