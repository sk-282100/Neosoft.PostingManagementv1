using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdDto>>
    {
        public string RoleId { get; set; }
    }
}
