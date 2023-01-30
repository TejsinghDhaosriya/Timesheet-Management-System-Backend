using TimesheetService.Models;

namespace TimesheetService.Repository
{
    public interface IProjectRepository
    {
        Project AddProject(Project project);
        List<Project> GetProjects();
        Project? GetProject(long id);
        void DeleteProject(Project project);
        Project? EditProject(Project project);
    }
}
