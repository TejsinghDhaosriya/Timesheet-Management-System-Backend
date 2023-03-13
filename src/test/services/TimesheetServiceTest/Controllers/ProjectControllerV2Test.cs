using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TimesheetService.Controllers;
using TimesheetService.DTOs.Request;
using TimesheetService.DTOs.Response;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetServiceTest.Controller
{
    public class ProjectControllerV2Test
    {
        [Fact]
        public void ShouldReturnGetAllSuccessResponseV2()
        {
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProjects()).Returns(new List<Project>());
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.GetProjects();
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Projects record found. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllProjectV2()
        {
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProjects()).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.GetProjects();
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception occured while fetching the projects record. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnGetByIdSuccessResponseV2()
        {
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Project record found. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnGetByIdFailedResponseWhenPassedIdDoesNotExistV2()
        {
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Project not found. ", timesheetServiceResponse.Message);
            Assert.Equal($"Project with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllByIdV2()
        {
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.GetProject(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception occured while fetching the project record. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnAddProjectSuccessResponseV2()
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
            var ProjectServ = new ProjectControllerV2(fakeProjectService);
            //ProjectServ.ControllerContext.HttpContext = fakeHttpContext; 
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Project created successfully. ", timesheetServiceResponse.Message);
            Assert.Equal(project, timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnAddProjectFailedResponseV2()
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
            var ProjectServ = new ProjectControllerV2(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (BadRequestObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(400, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Project is not created. ", timesheetServiceResponse.Message);
            Assert.Equal("Project Name already exist, please try with different name. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsEmptyV2()
        {
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(project);
            var ProjectServ = new ProjectControllerV2(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Header Values is required. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsNotAnIntegerV2()
        {
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("xyz");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Returns(project);
            var ProjectServ = new ProjectControllerV2(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Header values must be an integer value. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileAddingProjectV2()
        {
            Project project = new Project();
            var fakeHttpContext = A.Fake<HttpContext>();
            var expectedValue = new StringValues("123");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out expectedValue)).Returns(true);

            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.AddProject(project, 123)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);
            ProjectServ.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = ProjectServ.AddProject(project);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while adding a new project. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }


        [Fact]
        public void ShouldReturnDeleteProjectSuccessResponseV2()
        {
            Project project = new Project();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(project);
            A.CallTo(() => fakeProjectService.DeleteProject(project));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Project record is deactivate sucessfully. ", timesheetServiceResponse.Message);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnDeleteProjectFailedResponseV2()
        {
            Project project = new Project();
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Project record is not deleted. ", timesheetServiceResponse.Message);
            Assert.Equal($"Project with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileDeletngTheProjectV2()
        {
            Project project = new Project();
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.DeleteProject(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while deleting a project. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnUpdateProjectSuccessResponseV2()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            A.CallTo(() => fakeProjectService.UpdateProject(id, project)).Returns(new Project());
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Project record is updated sucessfully. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnUpdateProjectFailedResponseV2()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 0000;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Project record is not updated. ", timesheetServiceResponse.Message);
            Assert.Equal($"Project with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileUpdatingProjectV2()
        {
            ProjectUpdateRequest project = new ProjectUpdateRequest();
            long id = 1001;
            var fakeProjectService = A.Fake<IProjectService>();
            A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
            A.CallTo(() => fakeProjectService.UpdateProject(id, project)).Throws(new Exception("System Exception"));
            var ProjectServ = new ProjectControllerV2(fakeProjectService);

            var result = ProjectServ.UpdateProject(id, project);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while updating a project. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }
    }
}
