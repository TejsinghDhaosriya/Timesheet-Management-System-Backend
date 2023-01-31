using TimesheetService.DBContext;
using TimesheetService.DTOs.Request;
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
            project.CreatedAt = DateTime.UtcNow;
            project.ModifiedAt = DateTime.UtcNow;
            project.IsActive = true;
            _projectContext.Projects.Add(project);
            _projectContext.SaveChanges();
            return project;
        }

        public void DeleteProject(Project project)
        {
            var currentProject = _projectContext.Projects.Find(project.Id);
            if (currentProject != null)
            {
                currentProject.IsActive = false;
                _projectContext.Update(currentProject);
                _projectContext.SaveChanges();
            }
        }

        public Project? UpdateProject(long id, ProjectUpdateRequest project)
        {
            var currentProject = _projectContext.Projects.Find(id);
            if (currentProject != null)
            {
                if (project.Name != null)
                    currentProject.Name = project.Name;
                if(project.Description != null)
                    currentProject.Description = project.Description;
                if(project.StartDate != null)
                    currentProject.StartDate = (DateTime)project.StartDate;
                if(project.EndDate != null)
                    currentProject.EndDate = project.EndDate;
                if(project.Status != null)
                    currentProject.Status = (Process_Statuses)project.Status;
                if(project.ManagerId != null)
                    currentProject.ManagerId = (long)project.ManagerId;

                currentProject.ModifiedAt = DateTime.UtcNow;
                _projectContext.Update(currentProject);
                _projectContext.SaveChanges();
                var updatedProject = _projectContext.Projects.Find(id);
                return updatedProject;
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
            return _projectContext.Projects.Where(p => p.IsActive == true).ToList();
        }
    }
}
