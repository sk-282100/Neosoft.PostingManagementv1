using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;   
        }
        public async Task<Response<GetRoleByIdDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            string id = EncryptionDecryption.DecryptString(request.RoleId);

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
