using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Response<bool>>
    {
        public string RoleId { get; set; }
    }
}
