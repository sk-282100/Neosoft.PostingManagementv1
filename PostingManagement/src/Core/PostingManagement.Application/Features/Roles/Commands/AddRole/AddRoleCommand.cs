using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Commands.AddRole
{
    public class AddRoleCommand : IRequest<Response<bool>>
    {
        public string RoleName { get; set; }
    }
}
