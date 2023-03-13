using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TimesheetService.Controllers;
using TimesheetService.DTOs.Request;
using TimesheetService.DTOs.Response;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetServiceTest.Controllers
{
    public class ApprovalControllerV2Test
    {
        [Fact]
        public void ShouldReturnGetAllApprovalsSuccessResponseV2()
        {
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApprovals()).Returns(new List<Approval>());
            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.GetApprovals();
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Approvals record found. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllApprovalsV2()
        {
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApprovals()).Throws(new Exception("System Exception"));
            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.GetApprovals();
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception occured while fetching the approvals record. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnGetApprovalByIdSuccessResponseV2()
        {
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Approval record found. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);
        }

        [Fact]
        public void ShouldReturnGetApprovalByIdFailedResponseWhenPassedIdDoesNotExistV2()
        {
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Approval not found. ", timesheetServiceResponse.Message);
            Assert.Equal($"Approval with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetApprovalByIdV2()
        {
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Throws(new Exception("System Exception"));
            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.GetApproval(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception occured while fetching the approval record. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnDeleteApprovalSuccessResponseV2()
        {
            Approval approval = new Approval();
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(approval);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval));

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Approval record is deleted sucessfully. ", timesheetServiceResponse.Message);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnDeleteApprovalFailedResponseV2()
        {
            Approval approval = new Approval();
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval));

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Approval record is not deleted. ", timesheetServiceResponse.Message);
            Assert.Equal($"Approval with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInDeleteApprovalV2()
        {
            Approval approval = new Approval();
            long id = 1234;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(approval);
            A.CallTo(() => fakeApprovalService.DeleteApproval(approval)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.DeleteApproval(id);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while deleting a Approval. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnUpdateApprovalSuccessResponseV2()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 2001;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Returns(new Approval());

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Approval record is updated sucessfully. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturnUpdateApprovalFailedResponseV2()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 0000;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(null);
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Returns(new Approval());

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Approval record is not updated. ", timesheetServiceResponse.Message);
            Assert.Equal($"Approval with Id {id} was not found. ", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInUpdateApprovalV2()
        {
            ApprovalUpdateRequest approval = new ApprovalUpdateRequest();
            long id = 1234;
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.GetApproval(id)).Returns(new Approval());
            A.CallTo(() => fakeApprovalService.UpdateApproval(id, approval)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.UpdateApproval(id, approval);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while updating a Approval. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);
        }

        [Fact]
        public void ShouldReturnUpdateApprovalsSuccessResponseV2()
        {
            List<Approval> approvals = new List<Approval>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.UpdateApprovals(approvals)).Returns(approvals);

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.UpdateApprovals(approvals);
            var response = (OkObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(200, timesheetServiceResponse.StatusCode);
            Assert.Equal("Success: Approvals record is updated sucessfully. ", timesheetServiceResponse.Message);
            Assert.NotNull(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Warnings);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInUpdateApprovalsV2()
        {
            List<Approval> approvals = new List<Approval>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeApprovalService.UpdateApprovals(approvals)).Throws(new Exception("System Exception"));

            var ApprovalCont = new ApprovalControllerV2(fakeApprovalService);

            var result = ApprovalCont.UpdateApprovals(approvals);
            var response = (NotFoundObjectResult)result;
            var timesheetServiceResponse = (TimesheetServiceResponse)response.Value;

            Assert.Equal(404, timesheetServiceResponse.StatusCode);
            Assert.Equal("Fail: Exception Occured while updating Approvals. ", timesheetServiceResponse.Message);
            Assert.Equal("System Exception", timesheetServiceResponse.Error);
            Assert.Null(timesheetServiceResponse.Data);
            Assert.Null(timesheetServiceResponse.Warnings);

        }


    }
}
