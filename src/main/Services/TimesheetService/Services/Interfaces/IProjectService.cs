using TimesheetService.DTOs.Request;
using TimesheetService.Models;

namespace TimesheetService.Services.Interfaces
{
    public interface IProjectService
    {
        Project AddProject(Project project);
        List<Project> GetProjects();
        Project? GetProject(long id);
        void DeleteProject(Project project);
        Project? EditProject(long id, ProjectEditInputs project);
    }
}
