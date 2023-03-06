using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TimesheetService.Controllers;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetServiceTest.Controller
{
    public class ProjectControllerTest
    {
        [Fact]
        public void ShouldReturnGetAllSuccessResponse()
        {
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProjects()).Returns(new List<Project>());
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.GetProjects();
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllProject()
        {
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProjects()).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.GetProjects(); 
            var response = (NotFoundObjectResult)result;

             Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnGetByIdSuccessResponse()
        {
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturnGetByIdFailedResponseWhenPassedIdDoesNotExist()
        {
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllById()
        {
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnAddProjectSuccessResponse()
        {
            Project project = new Project()
            {
                Description = "this is demo description",
                Name = "demo",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ManagerId = new Guid(),
                OrganizationId = 123,
                Status = Process_Statuses.pending
            };
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("123");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(project);
            var ProjectServ = new ProjectController(fakeProjectService);
            //ProjectServ.ControllerContext.HttpContext = fakeHttpContext; 
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(project, response.Value);

        }

        [Fact]
        public void ShouldReturnAddProjectFailedResponse()
        {
            Project project = new Project()
            {
                Description = "this is demo description",
                Name = "demo",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ManagerId = new Guid(),
                OrganizationId = 123,
                Status = Process_Statuses.pending
            };
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("123");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(null);
            var ProjectServ = new ProjectController(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Project Name already exist, please try with different name", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsEmpty()
        { 
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(project);
            var ProjectServ = new ProjectController(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Header Values is required.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsNotAnInteger()
        {
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("xyz");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(project);
            var ProjectServ = new ProjectController(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Header values must be an integer value.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileAddingProject()
        {
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("123");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectController(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }


        [Fact]
        public void ShouldReturnDeleteProjectSuccessResponse()
        {
            Project project = new Project();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(project);
            A.CallTo(() => fakeProjectService.DeleteProject(project));
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
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
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileDeletngTheProject()
        {
            Project project = new Project();
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateProjectSuccessResponse()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            A.CallTo(() => fakeProjectService.UpdateProject(id, project)).Returns(new Project());
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Project record is updated sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateProjectFailedResponse()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (NotFoundObjectResult)result;

            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Project with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileUpdatingProject()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            A.CallTo(() => fakeProjectService.UpdateProject(id, project)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectController(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }
    }
}
