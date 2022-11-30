using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Roles.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        public EditRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            
        }
        public async Task<Response<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            string id = EncryptionDecryption.DecryptString(request.RoleId);
            //getting the Role object by RoleId
            Role roleToUpdate = await _roleRepository.GetRoleById(Convert.ToInt32(id));
            roleToUpdate.RoleName=request.RoleName;// assign the new value
            //Updating the Records
            await _roleRepository.UpdateAsync(roleToUpdate);
            return new Response<bool>() { Data = true, Message = "Role Updated Successfully", Succeeded = true };
        }
    }
}
