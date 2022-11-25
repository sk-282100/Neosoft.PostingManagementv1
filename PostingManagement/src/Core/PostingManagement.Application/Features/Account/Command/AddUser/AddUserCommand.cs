using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.AddUser
{
    public  class AddUserCommand : IRequest<Response<bool>>
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
    }
}
