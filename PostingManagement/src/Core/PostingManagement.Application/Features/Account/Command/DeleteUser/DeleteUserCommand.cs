using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.DeleteUser
{
    public  class DeleteUserCommand:IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public string DeleteBy { get; set; }
    }
}
