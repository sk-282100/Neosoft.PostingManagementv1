using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<Response<List<UserDetailsDto>>>
    {
    }
}
