using TimesheetService.DBContext;
using TimesheetService.Models;

namespace TimesheetService.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private TimeSheetDbContext _projectContext;
        public ProjectRepository(TimeSheetDbContext projectContext)
        {
            _projectContext = projectContext;
        }

        public Project AddProject(Project project)
        {
            _projectContext.Projects.Add(project);
            _projectContext.SaveChanges();
            return project;
        }

        public void DeleteProject(Project project)
        {
            var currentProject = _projectContext.Projects.Find(project.id);
            currentProject.is_active = false;
            _projectContext.Update(currentProject);
            _projectContext.SaveChanges();
        }

        public Project? EditProject(Project project)
        {
            var currentProject = _projectContext.Projects.Find(project.id);
            if (currentProject != null)
            {
                currentProject.name = project.name;
                currentProject.description = project.description;
                currentProject.start_date = project.start_date;
                currentProject.end_date = project.end_date;
                currentProject.total_time_spent = project.total_time_spent;
                currentProject.status = project.status;
                currentProject.manager_id = project.manager_id;
                currentProject.organization_id = project.organization_id;
                currentProject.is_active = project.is_active;
                _projectContext.Update(currentProject);
                _projectContext.SaveChanges();
                return currentProject;
            }
            return null;
        }

        public Project? GetProject(long id)
        {
            var project = _projectContext.Projects.Find(id);
            if (project != null)
            {
                return project;
            }
            return null;
        }

        public List<Project> GetProjects()
        {
            return _projectContext.Projects.ToList();
        }
    }
}
