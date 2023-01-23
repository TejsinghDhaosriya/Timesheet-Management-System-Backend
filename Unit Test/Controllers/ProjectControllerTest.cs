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
    }
}
