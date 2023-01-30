using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(_projectService.GetProjects());
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProject(long id)
        {
            var project = _projectService.GetProject(id);
            if (project != null)
            {
                return Ok(project);
            }
            return NotFound($"Project with Id {id} was not found.");
        }

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            var createdProject = _projectService.AddProject(project);
            //return Created(String.Format(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + project.id), project);
            return Ok(createdProject);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProject(long id)
        {
            var project = _projectService.GetProject(id);
            if (project != null)
            {
                _projectService.DeleteProject(project);
                return Ok("Project record is deactivate sucessfully. ");
            }
            return NotFound($"Project with Id {id} was not found.");
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditProject(long id, ProjectEditInputs project)
        {
            var currentProject = _projectService.GetProject(id);
            if (currentProject != null)
            {
                _projectService.EditProject(id,project);
                return Ok("Project record is updated sucessfully. ");
            }
            return NotFound($"Project with Id {id} was not found.");
        }
    }
}
