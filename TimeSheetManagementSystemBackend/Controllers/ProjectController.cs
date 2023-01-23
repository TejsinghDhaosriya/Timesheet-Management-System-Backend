using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheetManagementSystemBackend.Models;
using TimeSheetManagementSystemBackend.Repository_Layer;

namespace TimeSheetManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        [Route("/getAllProjects")]
        public IActionResult GetProjects()
        {
            return Ok(_projectRepository.GetProjects());
        }
        [HttpGet]
        [Route("/getProject/{id}")]
        public IActionResult GetProject(long id)
        {
            var project = _projectRepository.GetProject(id);
            if (project != null)
            {
                return Ok(project);
            }
            return NotFound($"Project with Id {id} was not found.");
        }

        [HttpPost]
        [Route("/addProject")]
        public IActionResult AddProject(Project project)
        {
            var createdProject = _projectRepository.AddProject(project);
            //return Created(String.Format(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + project.id), project);
            return Ok(createdProject);
        }

        [HttpDelete]
        [Route("deleteProject/{id}")]
        public IActionResult DeleteProject(long id)
        {
            var project = _projectRepository.GetProject(id);
            if (project != null)
            {
                _projectRepository.DeleteProject(project);
                return Ok("Project record is deactivate sucessfully. ");
            }
            return NotFound($"Project with Id {id} was not found.");
        }

        [HttpPatch]
        [Route("/updateProject/{id}")]
        public IActionResult EditProject(long id, Project project)
        {
            var currentProject = _projectRepository.GetProject(id);
            if (currentProject != null)
            {
                project.id = id;
                _projectRepository.EditProject(project);
                return Ok("Project record is updated sucessfully. ");
            }
            return NotFound($"Project with Id {project.id} was not found.");
        }
    }
}
