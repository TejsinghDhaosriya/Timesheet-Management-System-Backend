using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TimesheetService.Controllers;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetServiceTest.Controllers
{
    public class ApprovalControllerTest
    {
        [Fact]
        public void ShouldReturnGetAllApprovalsSuccessResponse()
        {
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApprovals()).Returns(new List<Approval>());
            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.GetApprovals();
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllApprovals()
        {
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApprovals()).Throws(new Exception("System Exception"));
            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.GetApprovals();
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnGetApprovalByIdSuccessResponse()
        {
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturnGetApprovalByIdFailedResponseWhenPassedIdDoesNotExist()
        {
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Approval with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetApprovalById()
        {
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Throws(new Exception("System Exception"));
            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnDeleteApprovalSuccessResponse()
        {
            Approval approval = new Approval();
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(approval);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval));

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Approval record is deleted sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnDeleteApprovalFailedResponse()
        {
            Approval approval = new Approval();
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval));

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Approval with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInDeleteApproval()
        {
            Approval approval = new Approval();
            long id = 1234;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(approval);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateApprovalSuccessResponse()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Returns(new Approval());

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Approval record is updated sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateApprovalFailedResponse()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Returns(new Approval());

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"Approval with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInUpdateApproval()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 1234;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateApprovalsSuccessResponse()
        {
            List<Approval> approvals = new List<Approval>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.UpdateApprovals(approvals)).Returns(approvals);

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.UpdateApprovals(approvals);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInUpdateApprovals()
        {
            List<Approval> approvals = new List<Approval>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.UpdateApprovals(approvals)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalController(fakeApprovalService);

            var result = ApprovalCont.UpdateApprovals(approvals);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }


    }
}
