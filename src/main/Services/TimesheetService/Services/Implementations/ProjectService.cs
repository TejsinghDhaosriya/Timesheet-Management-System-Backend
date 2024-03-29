﻿
using TimesheetService.DTOs.Request;
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

        public Project? AddProject(Project project, long OrganizationId)
        {
            return _projectRepository.AddProject(project, OrganizationId);
        }

        public void DeleteProject(Project project)
        {
            _projectRepository.DeleteProject(project);
        }

        public Project? UpdateProject(long id, ProjectUpdateRequest project)
        {
            return _projectRepository.UpdateProject(id, project);
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
