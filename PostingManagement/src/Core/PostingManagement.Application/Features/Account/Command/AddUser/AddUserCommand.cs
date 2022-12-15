using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.AddUser
{
    public  class AddUserCommand : IRequest<Response<bool>>
    {
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
    }
}
