using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetService.Controllers;
using TimesheetService.DTOs.Request;
using TimesheetService.Models;
using TimesheetService.Services.Interfaces;

namespace TimesheetServiceTest.Controllers
{
    public class TimesheetControllerTest
    {
        [Fact]
        public void ShouldReturnGetAllTimeSheetsSuccessResponse()
        {
            Guid userId = new Guid();
            long organizationId = 12345;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool withApproval = true;

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval))
                .Returns(new List<TimeSheet>());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetAllTimeSheets()
        {
            Guid userId = new Guid();
            long organizationId = 12345;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool withApproval = true;

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval))
                .Throws(new Exception("System Exception"));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.GetTimeSheets(userId, organizationId, startDate, endDate, withApproval);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnGetTimeSheetByIdSuccessResponse()
        {
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(new TimeSheet());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.GetTimeSheet(id);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(response.Value);

        }

        [Fact]
        public void ShouldReturnGetTimeSheetByIdFailedResponseWhenPassedIdDoesNotExist()
        {
            long id = 0000;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(null);
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.GetTimeSheet(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"TimeSheet with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionOccuredInGetTimeSheetById()
        {
            long id = 0000;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Throws(new Exception("System Exception"));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.GetTimeSheet(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

        [Fact]
        public void ShouldReturnAddTimeSeetSuccessResponse()
        {
           TimeSheet timeSheet = new TimeSheet();
           HeaderDTO headerValues = new HeaderDTO();
            var fakeHttpContext = A.Fake<HttpContext>();
            var user_id = new StringValues("0df67465-e8b8-423b-9076-122538a6d253");
            var organization_id = new StringValues("123");
            var project_id = new StringValues("25");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("user_id", out user_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out organization_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("project_id", out project_id)).Returns(true);

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeTimeSheetService.AddTimeSheet(timeSheet, headerValues)).Returns(timeSheet);
            A.CallTo(() => fakeApprovalService.AddApproval(timeSheet, headerValues)).Returns(new Approval());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);
            TimeSheetCont.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = TimeSheetCont.AddTimeSheet(timeSheet);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsEmptyInTimeSheet()
        {
            TimeSheet timeSheet = new TimeSheet();
            HeaderDTO headerValues = new HeaderDTO();
            var fakeHttpContext = A.Fake<HttpContext>();

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeTimeSheetService.AddTimeSheet(timeSheet, headerValues)).Returns(timeSheet);
            A.CallTo(() => fakeApprovalService.AddApproval(timeSheet, headerValues)).Returns(new Approval());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);
            TimeSheetCont.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = TimeSheetCont.AddTimeSheet(timeSheet);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Header Values is required.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenHeadersValueIsNotAnInteger()
        {
            TimeSheet timeSheet = new TimeSheet();
            HeaderDTO headerValues = new HeaderDTO();
            var fakeHttpContext = A.Fake<HttpContext>();
            var user_id = new StringValues("0df67465-e8b8-423b-9076-122538a6d253");
            var organization_id = new StringValues("xyz");
            var project_id = new StringValues("abc");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("user_id", out user_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out organization_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("project_id", out project_id)).Returns(true);

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            var fakeApprovalService = A.Fake<IApprovalService>();
            A.CallTo(() => fakeTimeSheetService.AddTimeSheet(timeSheet, headerValues)).Returns(timeSheet);
            A.CallTo(() => fakeApprovalService.AddApproval(timeSheet, headerValues)).Returns(new Approval());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);
            TimeSheetCont.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = TimeSheetCont.AddTimeSheet(timeSheet);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Header values must be an integer value.", response.Value);
        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileAddingTimeSheet()
        {
            TimeSheet timeSheet = new TimeSheet();
            HeaderDTO headerValues = new HeaderDTO();
            var fakeHttpContext = A.Fake<HttpContext>();
            var user_id = new StringValues("0df67465-e8b8-423b-9076-122538a6d253");
            var organization_id = new StringValues("12323456789098765444445678");
            var project_id = new StringValues("101");
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("user_id", out user_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("organization_id", out organization_id)).Returns(true);
            A.CallTo(() => fakeHttpContext.Request.Headers.TryGetValue("project_id", out project_id)).Returns(true);

            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.AddTimeSheet(timeSheet, headerValues)).Throws(new Exception("System Exception"));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);
            TimeSheetCont.ControllerContext = new ControllerContext()
            {
                HttpContext = fakeHttpContext
            };

            var result = TimeSheetCont.AddTimeSheet(timeSheet);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public void ShouldReturnDeleteTimeSheetSuccessResponse()
        {
            TimeSheet timeSheet = new TimeSheet();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(timeSheet);
            A.CallTo(() => fakeTimeSheetService.DeleteTimeSheet(timeSheet));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.DeleteTimeSheet(id);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("TimeSheet record is deleted sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnDeleteTimeSheetFailedResponse()
        {
            TimeSheet timeSheet = new TimeSheet();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(null);
            A.CallTo(() => fakeTimeSheetService.DeleteTimeSheet(timeSheet));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.DeleteTimeSheet(id);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"TimeSheet with Id {id} was not found.", response.Value);
        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileDeletngTheTimeSheet()
        {
            TimeSheet timeSheet = new TimeSheet();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(timeSheet);
            A.CallTo(() => fakeTimeSheetService.DeleteTimeSheet(timeSheet)).Throws(new Exception("System Exception"));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.DeleteTimeSheet(id);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);
        }

        [Fact]
        public void ShouldReturnUpdateTimeSheetSuccessResponse()
        {
            TimesheetUpdateRequest timeSheet = new TimesheetUpdateRequest();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(new TimeSheet());
            A.CallTo(() => fakeTimeSheetService.UpdateTimeSheet(id, timeSheet)).Returns(new TimeSheet());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.UpdateTimeSheet(id, timeSheet);
            var response = (OkObjectResult)result;

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("TimeSheet record is updated sucessfully. ", response.Value);

        }

        [Fact]
        public void ShouldReturnUpdateTimeSheetFailedResponse()
        {
            TimesheetUpdateRequest timeSheet = new TimesheetUpdateRequest();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(null);
            A.CallTo(() => fakeTimeSheetService.UpdateTimeSheet(id, timeSheet)).Returns(new TimeSheet());
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.UpdateTimeSheet(id, timeSheet);
            var response = (NotFoundObjectResult)result;

            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode);
            Assert.Equal($"TimeSheet with Id {id} was not found.", response.Value);

        }

        [Fact]
        public void ShouldReturn404ErrorWhenExceptionIsOccuredWhileUpdatingTimeSheet()
        {
            TimesheetUpdateRequest timeSheet = new TimesheetUpdateRequest();
            long id = 1001;
            var fakeTimeSheetService = A.Fake<ITimeSheetService>();
            A.CallTo(() => fakeTimeSheetService.GetTimeSheet(id)).Returns(new TimeSheet());
            A.CallTo(() => fakeTimeSheetService.UpdateTimeSheet(id, timeSheet)).Throws(new Exception("System Exception"));
            var TimeSheetCont = new TimeSheetController(fakeTimeSheetService);

            var result = TimeSheetCont.UpdateTimeSheet(id, timeSheet);
            var response = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("System Exception", response.Value);

        }

    }
}
