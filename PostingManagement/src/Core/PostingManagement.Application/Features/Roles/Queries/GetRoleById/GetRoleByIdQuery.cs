using MediatR;
using PostingManagement.Application.Features.Events.Queries.GetEventDetail;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdDto>>
    {
        public string RoleId { get; set; }
    }
}
