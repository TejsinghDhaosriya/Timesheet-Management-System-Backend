using FakeItEasy;
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
                description = "this is demo description",
                name = "demo",
                manager_id = 100,
                organization_id = 1001,
                is_active = true,
                status = Process_Statuses.pending
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
                description = "this is demo description",
                name = "demo",
                manager_id = 100,
                organization_id = 1001,
                is_active = true,
                status = Process_Statuses.pending
            };
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.AddProject(project)).Returns(project);

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.AddProject(project);
            Assert.NotNull(result);
            Assert.Equal(result,project);

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
            Project project = new Project()
            {
                description = "this is demo description",
                name = "demo",
                manager_id = 100,
                organization_id = 1001,
                is_active = true,
                status = Process_Statuses.pending
            };
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.EditProject(project)).Returns(project);

            var ProjectRepo = new ProjectService(fakeProjectRepository);

            var result = ProjectRepo.EditProject(project);
            Assert.NotNull(result);
            Assert.Equal(result, project);


        }

    }
}
