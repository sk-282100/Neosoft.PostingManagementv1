using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist
{
    public class IsRoleAlreadyExistQueryHandler : IRequestHandler<IsRoleAlreadyExistQuery, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        public IsRoleAlreadyExistQueryHandler( IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Response<bool>> Handle(IsRoleAlreadyExistQuery request, CancellationToken cancellationToken)
        {
            bool roleExist = await _roleRepository.IsRoleAlreadyExist(request.RoleName);
            return new Response<bool>() { Succeeded = true, Data = roleExist };
        }
    }
}
