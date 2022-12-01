using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<Response<List<GetAllRolesDto>>>
    {
    }
}
