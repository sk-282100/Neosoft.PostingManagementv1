using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById;
using PostingManagement.Application.Features.TransferList.Queries.GetTransferList;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class TransferListControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        public TransferListControllerTest()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task Get_GetTransferList()
        {
            var controller = new TransferListController(_mockMediator.Object);

            var result = await controller.GetTransferList(new GetTransferListQuery());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<TransferListReponse>>();
        }

        [Fact]
        public async Task Get_GetAdditionalEmployeeDetails()
        {
            var controller = new TransferListController(_mockMediator.Object);
            int employeeId = 12;
            string movementType = "Inter Zone Request";
            var result = await controller.GetAdditionalEmployeeDetails(employeeId,movementType);

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<EmployeeDetailsForTransferList>>();
        }

        [Fact]
        public async Task Get_GetTransferListByEmployeeId()
        {
            var controller = new TransferListController(_mockMediator.Object);
            var employeeIdList = new List<int>();
            var result = await controller.GetTransferListByEmployeeId(employeeIdList);

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<TransferListVM>>>();
        }

        [Fact]
        public async Task Get_MatchRequestTransferVacancy()
        {
            var controller = new TransferListController(_mockMediator.Object);
            var employeeIdList = new List<int>();
            var result = await controller.MatchRequestTransferVacancy(employeeIdList);

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<MatchingRequestTransferVacancy>>>();
        }

        [Fact]
        public async Task Post_TransferListForZO()
        {
            var controller = new TransferListController(_mockMediator.Object);
            var transferList = new List<TransferListVM>();
            var result = await controller.TransferListForZO(transferList);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ZOTransferListReponse>>();
            
        }
    }
}
