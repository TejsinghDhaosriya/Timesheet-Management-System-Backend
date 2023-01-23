using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetManagementSystemBackend.Controllers;
using TimeSheetManagementSystemBackend.Models;
using TimeSheetManagementSystemBackend.Repository_Layer;
using Xunit;

namespace Unit_Test.Controllers
{
    public class ProjectControllerTest
    {
        [Fact]
        public void ShouldReturnGetAllSuccessResponse()
        {
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProjects()).Returns(new List<Project>());

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.GetProjects();
            Assert.NotNull(result);

            var response = (OkObjectResult)result;
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturnGetByIdSuccessResponse()
        {
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(new Project());

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.GetProject(id);
            Assert.NotNull(result);

            var response = (OkObjectResult)result;
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturnGetByIdfailedResponseWhenWrongIdIsPassed()
        {
            long id = 0000;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(null);

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.GetProject(id);
            Assert.NotNull(result);

            var response = (NotFoundObjectResult)result;
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturnAddProjectSuccessResponse()
        {
            Project project = new Project() {
                description = "this is demo description",
                name = "demo",
                manager_id = 100,
                organization_id = 1001,
                is_active = true,
                status = Process_Statuses.pending
            };
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.AddProject(project)).Returns(project);

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.AddProject(project);
            Assert.NotNull(result);

            var response = (OkObjectResult)result;
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturnDeleteProjectSuccessResponse()
        {
            Project project = new Project();
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(project);
            A.CallTo(() => fakeProjectRepository.DeleteProject(project));

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.DeleteProject(id);
            Assert.NotNull(result);

            var response = (OkObjectResult)result;
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Project record is deactivate sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnDeleteProjectFailedResponse()
        {
            Project project = new Project();
            long id = 0000;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(null);

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.DeleteProject(id);
            Assert.NotNull(result);

            var response = (NotFoundObjectResult)result;
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateProjectSuccessResponse()
        {
            Project project = new Project();
            long id = 1001;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(project);
            A.CallTo(() => fakeProjectRepository.EditProject(project)).Returns(project);

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.EditProject(id, project);
            Assert.NotNull(result);

            var response = (OkObjectResult)result;
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Project record is updated sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateProjectFailedResponse()
        {
            Project project = new Project();
            long id = 0000;
            var fakeProjectRepository = A.Fake<IProjectRepository>();
            A.CallTo(() => fakeProjectRepository.GetProject(id)).Returns(null);

            var ProjectRepo = new ProjectController(fakeProjectRepository);

            var result = ProjectRepo.EditProject(id, project);
            Assert.NotNull(result);

            var response = (NotFoundObjectResult)result;
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }
    }
}
