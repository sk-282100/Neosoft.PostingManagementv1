using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.Triggers.Commands.AddTrigger;
using PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;
using PostingManagement.Application.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class TriggerControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        public TriggerControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task Add_NewTrigger()
        {
            var controller = new TriggerController(_mockMediator.Object);

            var result = await controller.AddTrigger(new AddTriggerCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Update_Trigger()
        {
            var controller = new TriggerController(_mockMediator.Object);

            var result = await controller.UpdateTrigger(new UpdateTriggerCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Delete_Trigger()
        {
            var controller = new TriggerController(_mockMediator.Object);

            var result = await controller.DeleteTrigger("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Get_All_Trigger()
        {
            var controller = new TriggerController(_mockMediator.Object);

            var result = await controller.GetAllTrigger();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<GetAllTriggerDto>>>();
        }

        [Fact]
        public async Task Get_Trigger_ById()
        {
            var controller = new TriggerController(_mockMediator.Object);

            var result = await controller.GetTriggerById("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<GetTriggerByIdDto>>();
        }
    }
}
