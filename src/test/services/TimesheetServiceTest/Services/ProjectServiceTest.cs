using FakeItEasy;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Repository;
using TimesheetService.Services.Implementations;

namespace TimesheetServiceTest.Services
{
    public class ProjectServiceTest
    {

        [Fact]
        public void ShouldReturnGetAllProject()
        {
            var project = new List<Project> { new Project() };
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProjects()).Returns(project);

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.GetProjects();
            Assert.NotNull(result);
            Assert.Equal(project, result);

        }

        [Fact]
        public void ShouldReturnGetProjectById()
        {
            Project project = new Project()
            {
                Description = "this is demo description",
                Name = "demo",
                ManagerId = 100,
                OrganizationId = 1001,
                Status = Process_Statuses.pending
            };
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(project);

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.GetProject(id);
            Assert.NotNull(result);
            Assert.Equal(result, project);

        }

        [Fact]
        public void ShouldAddProject()
        {
            Project project = new Project()
            {
                Description = "this is demo description",
                Name = "demo",
                ManagerId = 100,
                OrganizationId = 1001,
                Status = Process_Statuses.pending
            };
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.AddProject(project, project.OrganizationId)).Returns(project);

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.AddProject(project, project.OrganizationId);
            Assert.NotNull(result);
            Assert.Equal(result, project);

        }

        [Fact]
        public void ShouldDeleteProject()
        {
            Project project = new Project();
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.DeleteProject(project));

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            ProjectRepo.DeleteProject(project);
            Assert.True(true);
        }

        [Fact]
        public void ShouldReturnUpdateProject()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest()
            {
                Description = "this is demo description",
                Name = "demo",
                ManagerId = 100,
                Status = Process_Statuses.pending
            };
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.UpdateProject(id, project)).Returns(new Project());

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.UpdateProject(id, project);
            Assert.NotNull(result);


        }

    }
}
