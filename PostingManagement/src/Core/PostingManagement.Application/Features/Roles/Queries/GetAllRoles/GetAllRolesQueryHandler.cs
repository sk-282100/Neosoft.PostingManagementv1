using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesDto>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetAllRolesQueryHandler(IRoleRepository roleRepository,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;   
        }

        public async Task<Response<List<GetAllRolesDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllRoles();
            //checking Role List is empty
            if (roles.Count != 0)
            {
                List<GetAllRolesDto> rolesDto = _mapper.Map<List<GetAllRolesDto>>(roles);
                return new Response<List<GetAllRolesDto>>() { Data = rolesDto, Succeeded = true };
            }
            else
            {
                return new Response<List<GetAllRolesDto>>() { Message = "No Role Exists", Succeeded = false };
            }
        }
    }
}
