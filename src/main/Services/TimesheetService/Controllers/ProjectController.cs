using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Controllers
{
    [Route("api/v1/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public IActionResult GetProjects()
        {
            try
            {
                return Ok(_projectService.GetProjects());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
                    return Ok(project);
                }
                return NotFound($"Project with Id {id} was not found.");
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
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
                    return Ok(createdProject);
                return BadRequest("Project Name already exist, please try with different name");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    return Ok("Project record is deactivate sucessfully. ");
                }
                return NotFound($"Project with Id {id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    _projectService.UpdateProject(id, project);
                    return Ok("Project record is updated sucessfully. ");
                }
                return NotFound($"Project with Id {id} was not found.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
