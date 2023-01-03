using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.Account.Command.AddUser;
using PostingManagement.Application.Features.Account.Command.DeleteUser;
using PostingManagement.Application.Features.Account.Command.EditUser;
using PostingManagement.Application.Features.Account.Command.ResetPassword;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Application.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class AccountControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        public AccountControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
        }

        [Fact]
        public async Task Add_User()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.AddUser(new AddUserCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Delete_User()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.DeleteUser(new DeleteUserCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task GetAll_User()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.GetAllUser();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<GetAllUserDetailsDto>>>();
        }

        [Fact]
        public async Task GetUserDetails_ById()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.GetUserDetailsById("123");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<UserDetailsDto>>();
        }

        [Fact]
        public async Task Is_UserName_Present()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.IsUserNamePresent("abc");

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Reset_Password()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.ResetPassword(new ResetPasswordCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }

        [Fact]
        public async Task Edit_User()
        {
            var controller = new AccountController(_mockMediator.Object);

            var result = await controller.EditUser(new EditUserCommad());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }
    }
}
