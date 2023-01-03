using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.JobFamily.Commands.AddJobFamily;
using PostingManagement.Application.Features.JobFamily.Commands.EditJobFamily;
using PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery;
using PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById;
using PostingManagement.Application.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class JobFamilyControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;

        public JobFamilyControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task GetAll_JobFamily()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.GetAllJobFamily();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<GetAllJobFamilyDto>>>();
        }

        [Fact]
        public async Task Add_JobFamily()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.AddJobFamily(new AddjobFamilyCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Delete_JobFamily()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.DeleteJobFamily("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Edit_JobFamily()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.EditJobFamily(new EditJobFamilyCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Get_JobFamily_By_Id()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.GetJobFamilyById("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<GetjobFamilyByIdDto>>();
        }

        [Fact]
        public async Task IsJobFamily_Exist()
        {
            var controller = new JobFamilyController(_mockMediator.Object);

            var result = await controller.IsJobFamilyAlreadyExist("Jobfamily");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }
    }
}
