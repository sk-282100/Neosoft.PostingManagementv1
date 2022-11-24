using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<List<Role>>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Response<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            List<Role> roles = new List<Role>();
            roles = await _roleRepository.GetAllRoles();
            if (roles.Count != 0)
            {
                return new Response<List<Role>>() { Data = roles, Succeeded = true };
            }
            else
            {
                return new Response<List<Role>>() { Message = "No Role Exists", Succeeded = false };
            }
        }
    }
}
