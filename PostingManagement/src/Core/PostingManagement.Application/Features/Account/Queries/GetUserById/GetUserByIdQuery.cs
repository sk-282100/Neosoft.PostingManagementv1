using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdQuery :IRequest<Response<UserDetailsDto>>
    {
        public string UId { get; set; } 
    }
}
