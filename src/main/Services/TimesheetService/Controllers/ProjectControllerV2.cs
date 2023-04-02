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
    [Route("api/v2/project")]
    [ApiController]
    public class ProjectControllerV2 : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectControllerV2(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _projectService.GetProjects();
                return Ok(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success: Projects record found. ",
                    Data = projects
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the projects record. ",
                    Error = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProject(long id)
        {
            try
            {
                var project = _projectService.GetProject(id);
                if (project != null)
                {
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Project record found. ",
                        Data = project
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Project not found. ",
                    Error = $"Project with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception occured while fetching the project record. ",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            try
            {
                Request.Headers.TryGetValue("organization_id", out StringValues headerValue);
                long OrganizationId = long.Parse(headerValue);
                var createdProject = _projectService.AddProject(project, OrganizationId);
                if (createdProject != null)
                {
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Project created successfully. ",
                        Data = createdProject
                    });
                }
                return BadRequest(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Fail: Project is not created. ",
                    Error = "Project Name already exist, please try with different name. "
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
                    Message = "Fail: Exception Occured while adding a new project. ",
                    Error = ex.Message
                });
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProject(long id)
        {
            try
            {
                var project = _projectService.GetProject(id);
                if (project != null)
                {
                    _projectService.DeleteProject(project);
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Project record is deactivate sucessfully. "
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Project record is not deleted. ",
                    Error = $"Project with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while deleting a project. ",
                    Error = ex.Message
                });
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateProject(long id, ProjectUpdateRequest project)
        {
            try
            {
                var currentProject = _projectService.GetProject(id);
                if (currentProject != null)
                {
                   var updatedProject = _projectService.UpdateProject(id, project);
                    return Ok(new TimesheetServiceResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success: Project record is updated sucessfully. ",
                        Data = updatedProject
                    });
                }
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Project record is not updated. ",
                    Error = $"Project with Id {id} was not found. "
                });
            }
            catch (Exception ex)
            {
                return NotFound(new TimesheetServiceResponse()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Fail: Exception Occured while updating a project. ",
                    Error = ex.Message
                });
            }
        }
    }
}
