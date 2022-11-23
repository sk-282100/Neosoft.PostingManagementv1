using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Command.EditUser
{
    public  class EditUserCommad :IRequest<Response<bool>>
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UpdatedBy { get; set; }
      
    }
}
