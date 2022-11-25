using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist
{
    public class IsRoleAlreadyExistQueryHandler : IRequestHandler<IsRoleAlreadyExistQuery, Response<bool>>
    {
        private readonly IMapper _mapper;   
        private readonly IRoleRepository _roleRepository;
            public IsRoleAlreadyExistQueryHandler(IMapper mapper, IRoleRepository roleRepository)
            {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<Response<bool>> Handle(IsRoleAlreadyExistQuery request, CancellationToken cancellationToken)
        {
            bool roleExist = await _roleRepository.IsRoleAlreadyExist(request.RoleName);
            return new Response<bool>() { Succeeded = true , Data = roleExist };
        }
    }
}
