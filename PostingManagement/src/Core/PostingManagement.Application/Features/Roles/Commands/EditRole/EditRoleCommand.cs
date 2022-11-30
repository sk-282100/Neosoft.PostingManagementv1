using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Commands.EditRole
{
    public class EditRoleCommand : IRequest<Response<bool>>
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
