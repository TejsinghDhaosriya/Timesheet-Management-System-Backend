// using FakeItEasy;
// using Microsoft.AspNetCore.Mvc;
// using TimesheetService.Controllers;
// using TimesheetService.DTOs.Request;
// using TimesheetService.Models;
// using TimesheetService.Services.Interfaces;
//
// namespace TimesheetServiceTest.Controller
// {
//     public class ProjectControllerTest
//     {
//         [Fact]
//         public void ShouldReturnGetAllSuccessResponse()
//         {
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProjects()).Returns(new List<Project>());
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.GetProjects();
//
//             Assert.NotNull(result);
//             var response = (OkObjectResult)result;
//             Assert.IsType<OkObjectResult>(response);
//             Assert.Equal(200, response.StatusCode);
//
//         }
//
//         [Fact]
//         public void ShouldReturnGetByIdSuccessResponse()
//         {
//             long id = 1001;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.GetProject(id);
//             Assert.NotNull(result);
//
//             var response = (OkObjectResult)result;
//             Assert.IsType<OkObjectResult>(response);
//             Assert.Equal(200, response.StatusCode);
//
//         }
//
//         [Fact]
//         public void ShouldReturnGetByIdfailedResponseWhenWrongIdIsPassed()
//         {
//             long id = 0000;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.GetProject(id);
//             Assert.NotNull(result);
//
//             var response = (NotFoundObjectResult)result;
//             Assert.IsType<NotFoundObjectResult>(response);
//             Assert.Equal(404, response.StatusCode);
//             Assert.Equal($"Project with Id {id} was not found.", response.Value);
//
//         }
//
//         [Fact]
//         public void ShouldReturnAddProjectSuccessResponse()
//         {
//             Project project = new Project()
//             {
//                 Description = "this is demo description",
//                 Name = "demo",
//                 ManagerId = 100,
//                 OrganizationId = 1001,
//                 Status = Process_Statuses.pending
//             };
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.AddProject(project, project.OrganizationId)).Returns(project);
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.AddProject(project);
//             Assert.NotNull(result);
//
//             var response = (OkObjectResult)result;
//             Assert.IsType<OkObjectResult>(response);
//             Assert.Equal(200, response.StatusCode);
//
//         }
//
//         [Fact]
//         public void ShouldReturnDeleteProjectSuccessResponse()
//         {
//             Project project = new Project();
//             long id = 1001;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(project);
//             A.CallTo(() => fakeProjectService.DeleteProject(project));
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.DeleteProject(id);
//             Assert.NotNull(result);
//
//             var response = (OkObjectResult)result;
//             Assert.IsType<OkObjectResult>(response);
//             Assert.Equal(200, response.StatusCode);
//             Assert.Equal("Project record is deactivate sucessfully. ", response.Value);
//
//         }
//
//         [Fact]
//         public void ShouldReturnDeleteProjectFailedResponse()
//         {
//             Project project = new Project();
//             long id = 0000;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.DeleteProject(id);
//             Assert.NotNull(result);
//
//             var response = (NotFoundObjectResult)result;
//             Assert.IsType<NotFoundObjectResult>(response);
//             Assert.Equal(404, response.StatusCode);
//             Assert.Equal($"Project with Id {id} was not found.", response.Value);
//
//         }
//
//         [Fact]
//         public void ShouldReturnUpdateProjectSuccessResponse()
//         {
//             // Project project = new Project();
//             ProjectUpdateRequest project = new ProjectUpdateRequest();
//             long id = 1001;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(new Project());
//             A.CallTo(() => fakeProjectService.UpdateProject(id, project)).Returns(new Project());
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.UpdateProject(id, project);
//             Assert.NotNull(result);
//
//             var response = (OkObjectResult)result;
//             Assert.IsType<OkObjectResult>(response);
//             Assert.Equal(200, response.StatusCode);
//             Assert.Equal("Project record is updated sucessfully. ", response.Value);
//
//         }
//
//         [Fact]
//         public void ShouldReturnUpdateProjectFailedResponse()
//         {
//             ProjectUpdateRequest project = new ProjectUpdateRequest();
//             long id = 0000;
//             var fakeProjectService = A.Fake<IProjectService>();
//             A.CallTo(() => fakeProjectService.GetProject(id)).Returns(null);
//
//             var ProjectServ = new ProjectController(fakeProjectService);
//
//             var result = ProjectServ.UpdateProject(id, project);
//             Assert.NotNull(result);
//
//             var response = (NotFoundObjectResult)result;
//             Assert.Equal(404, response.StatusCode);
//             Assert.Equal($"Project with Id {id} was not found.", response.Value);
//
//         }
//     }
// }
