﻿using TimesheetService.DBContext;
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

        public Project EditProject(Project project)
        {
            var currentProject = _projectContext.Projects.Find(project.id);
            currentProject.name = project.name;
            _projectContext.Update(currentProject);
            _projectContext.SaveChanges();
            return currentProject;
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
