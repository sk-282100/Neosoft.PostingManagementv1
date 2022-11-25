using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
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
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IDataProtector _protector;
        private readonly IMapper _mapper;
        
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository,IDataProtectionProvider provider,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _protector = provider.CreateProtector("");   
            _mapper = mapper;   
        }
        public async Task<Response<GetRoleByIdDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            string id = _protector.Unprotect(request.RoleId);

            Role role = await _roleRepository.GetRoleById(Convert.ToInt32(id));
            if(role != null)
            {
                var rolesDto = _mapper.Map<GetRoleByIdDto>(role);
                return new Response<GetRoleByIdDto>() { Data = rolesDto, Succeeded = true };
            }
            else
            {
                return new Response<GetRoleByIdDto>() { Message = "Role Does not Exists", Succeeded = false };
            }
        }
    }
}
