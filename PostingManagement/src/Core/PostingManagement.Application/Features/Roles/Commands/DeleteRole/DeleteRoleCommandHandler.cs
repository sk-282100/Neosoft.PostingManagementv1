using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response<bool>>
    {
        private IRoleRepository _roleRepository;    
        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
               
        }
        public async Task<Response<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            string id = EncryptionDecryption.EncryptString(request.RoleId);
            Role role = await _roleRepository.GetRoleById(Convert.ToInt32(id));

            //checking if Role is Present then delete the Role 
            if(role != null)
            { 
                bool result = await _roleRepository.DeleteAsync(role);
                return new Response<bool>() { Data = result, Message = "Role Deleted Successfully", Succeeded = true };
            }
            else
            {
                return new Response<bool>() { Data = false, Message = "Role Does Not Exists", Succeeded = false };
            }
        }        
    }
}
