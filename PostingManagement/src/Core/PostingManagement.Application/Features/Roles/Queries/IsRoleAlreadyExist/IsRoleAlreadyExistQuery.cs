using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist
{
    public class IsRoleAlreadyExistQuery :IRequest<Response<bool>>
    {
        public string RoleName { get; set; }
    }
}
