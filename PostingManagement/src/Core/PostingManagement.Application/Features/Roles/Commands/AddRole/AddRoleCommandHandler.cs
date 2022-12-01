using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.Roles.Commands.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Response<bool>>
    {
        private IRoleRepository _roleRepository;        
        public AddRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;            
        }

        public async Task<Response<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetRoleByName(request.RoleName);
            if(role == null)
            {
                bool result = await _roleRepository.AddAsync(request.RoleName);
                return new Response<bool>() {Data = result, Message = "Role Added Successfully", Succeeded = true };                                
            }
            else
            {
                return new Response<bool>() { Data = false, Message = "Role Already Exists", Succeeded = false };
            }            
        }
    }
}
