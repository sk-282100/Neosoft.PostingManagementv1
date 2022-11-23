using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Command.DeleteUser
{
    public  class DeleteUserCommand:IRequest<Response<bool>>
    {
        public int UserId { get; set; }
        public string DeleteBy { get; set; }
    }
}
