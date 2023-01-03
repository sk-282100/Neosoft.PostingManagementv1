using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.Scales.Queries.GetAllScales;
using PostingManagement.Application.Responses;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class ScaleControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        public ScaleControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task Get_All_Scales()
        {
            var controller = new ScaleController(_mockMediator.Object);

            var result = await controller.GetAllScaleDetails();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<ScaleDto>>>();
        }
    }
}
