using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
using PostingManagement.Application.Features.Roles.Commands.DeleteRole;
using PostingManagement.Application.Features.Roles.Commands.EditRole;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddUserRole (AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteUserRole(string id)
        {
            DeleteRoleCommand command = new DeleteRoleCommand() { RoleId=id};
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut("EditRole")]
        public async Task<IActionResult> EditUserRole(EditRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("GetRoleById")]       
        public async Task<IActionResult> GetRoleById(string id)
        {
            GetRoleByIdQuery query = new GetRoleByIdQuery { RoleId = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            GetAllRolesQuery query = new GetAllRolesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("IsRoleAlreadyExist")]
         public async Task<IActionResult> IsRoleAlreadyExist(string roleName)
         {
            IsRoleAlreadyExistQuery query = new IsRoleAlreadyExistQuery { RoleName = roleName};
            var response = await _mediator.Send(query);
            return Ok(response);
        } 
        
        
       
    }
}
