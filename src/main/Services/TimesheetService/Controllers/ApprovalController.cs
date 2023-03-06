using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v1/timesheet/approval")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private IApprovalService _approvalService;
        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }
        [HttpGet]
        public IActionResult GetApprovals()
        {
            try
            {
                return Ok(_approvalService.GetApprovals());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
                    return Ok(approval);
                }
                return NotFound($"Approval with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
                    return Ok("Approval record is deleted sucessfully. ");
                }
                return NotFound($"Approval with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    _approvalService.UpdateApproval(id, approval);
                    return Ok("Approval record is updated sucessfully. ");
                }
                return NotFound($"Approval with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("/api/v1/timesheet/approvals")]
        public IActionResult UpdateApprovals(List<Approval> approvals)
        {
            try
            {
                var updatedApproval = _approvalService.UpdateApprovals(approvals);
                return Ok(updatedApproval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
