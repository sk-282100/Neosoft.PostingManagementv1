using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.EditUser
{
    public  class EditUserCommad :IRequest<Response<bool>>
    {
        public string UId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string UpdatedBy { get; set; }
      
    }
}
