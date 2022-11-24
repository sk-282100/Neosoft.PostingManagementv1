using MediatR;
using PostingManagement.Application.Contracts.Persistence;
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
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<Role>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Response<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetRoleById(request.RoleId);
            if(role != null)
            {
                return new Response<Role>() { Data = role, Succeeded = true };
            }
            else
            {
                return new Response<Role>() { Message = "Role Does not Exists", Succeeded = false };
            }
        }
    }
}
