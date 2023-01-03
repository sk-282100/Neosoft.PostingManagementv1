using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
using PostingManagement.Application.Features.Roles.Commands.EditRole;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class RoleControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;

        public RoleControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task Add_Role()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.AddUserRole(new AddRoleCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Delete_Role()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.DeleteUserRole("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }


        [Fact]
        public async Task Edit_UserRole()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.EditUserRole(new EditRoleCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }


        [Fact]
        public async Task Get_RoleById()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.GetRoleById("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<GetRoleByIdDto>>();
        }


        [Fact]
        public async Task GetAll_Role()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.GetAllRoles();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<GetAllRolesDto>>>();
        }

        [Fact]
        public async Task IsRole_Exist()
        {
            var controller = new RoleController(_mockMediator.Object);

            var result = await controller.IsRoleAlreadyExist("abc");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

    }
}
