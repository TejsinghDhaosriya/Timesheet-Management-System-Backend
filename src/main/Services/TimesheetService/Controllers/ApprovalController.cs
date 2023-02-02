using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v1/approval")]
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
            return Ok(_approvalService.GetApprovals());
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetApproval(long id)
        {
            var approval = _approvalService.GetApproval(id);
            if (approval != null)
            {
                return Ok(approval);
            }
            return NotFound($"Approval with Id {id} was not found.");
        }

        [HttpPost]
        public IActionResult AddApproval(TimeSheet approval, [FromHeader]HeaderDTO headerValues)
        {
            var createdApproval = _approvalService.AddApproval(approval, headerValues);
            return Ok(createdApproval);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteApproval(long id)
        {
            var approval = _approvalService.GetApproval(id);
            if (approval != null)
            {
                _approvalService.DeleteApproval(approval);
                return Ok("Approval record is deleted sucessfully. ");
            }
            return NotFound($"Approval with Id {id} was not found.");
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditApproval(long id, Approval approval)
        {
            var currentApproval = _approvalService.GetApproval(id);
            if (currentApproval != null)
            {
                approval.Id = id;
                _approvalService.EditApproval(approval);
                return Ok("Approval record is updated sucessfully. ");
            }
            return NotFound($"Approval with Id {approval.Id} was not found.");
        }
    }
}
