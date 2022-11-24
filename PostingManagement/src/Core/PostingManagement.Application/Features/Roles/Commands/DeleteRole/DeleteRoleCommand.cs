using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Response<bool>>
    {
        public int RoleId { get; set; }
    }
}
