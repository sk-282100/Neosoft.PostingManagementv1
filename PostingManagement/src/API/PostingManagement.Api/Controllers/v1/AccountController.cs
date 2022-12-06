using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.Account.Command.AddUser;
using PostingManagement.Application.Features.Account.Command.DeleteUser;
using PostingManagement.Application.Features.Account.Command.EditUser;
using PostingManagement.Application.Features.Account.Command.ResetPassword;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Application.Features.Account.Queries.IsUserNamePresent;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddUser(AddUserCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(EditUserCommad request)
        {
            var response = await _mediator.Send(request);
            if(response.Succeeded==false && response.Message == "User not Found")
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            GetAllUserQuery request = new GetAllUserQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserDetailsById(string Id)
        {
            GetUserByIdQuery request = new GetUserByIdQuery() { UId=Id};
            var response = await _mediator.Send(request);
            if (response.Succeeded == false && response.Data.UserName==null)
            {
                 return NotFound(response);
            } ;
            return Ok(response);
        }
        [HttpGet("IsUserNamePresent")]
        public async Task<IActionResult> IsUserNamePresent(string userName)
        {
            IsUserNamePresentQuery request = new IsUserNamePresentQuery() { UserName = userName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
        {
            var response = await _mediator.Send(request);
            if (response.Succeeded == false && response.Message == "Username is not Exist")
            {
                return NotFound(response);
            }
            return Ok(response);
        }


    }
}
