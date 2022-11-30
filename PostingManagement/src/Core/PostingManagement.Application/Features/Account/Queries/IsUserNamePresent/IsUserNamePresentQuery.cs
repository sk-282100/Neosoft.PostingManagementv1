using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Queries.IsUserNamePresent
{
    public class IsUserNamePresentQuery :IRequest<Response<bool>>
    {
        public string UserName { get; set; }
    }
}
