using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Repository
{
    public interface IProjectRepository
    {
        Project? AddProject(Project project, long OrganizationId);
        List<Project> GetProjects();
        Project? GetProject(long id);
        void DeleteProject(Project project);
        Project? UpdateProject(long id, ProjectUpdateRequest project);
    }
}
