
using TimesheetService.Models;
using TimesheetService.Repository;
using TimesheetService.Services.Interfaces;

namespace TimesheetService.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Project AddProject(Project project)
        {
            return _projectRepository.AddProject(project);
        }

        public void DeleteProject(Project project)
        {
            _projectRepository.DeleteProject(project);
        }

        public Project? EditProject(Project project)
        {
            return _projectRepository.EditProject(project);
        }


        public Project? GetProject(long id)
        {
            return _projectRepository.GetProject(id);
        }

        public List<Project> GetProjects()
        {
            return _projectRepository.GetProjects();
        }
    }
}
