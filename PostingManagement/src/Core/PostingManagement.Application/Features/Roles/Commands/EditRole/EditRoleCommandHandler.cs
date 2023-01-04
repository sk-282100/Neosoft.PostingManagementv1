using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Roles.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IDataProtector _dataProtector;
        public EditRoleCommandHandler(IRoleRepository roleRepository,IDataProtectionProvider provider)
        {
            _roleRepository = roleRepository;
            _dataProtector = provider.CreateProtector("");
            
        }
        public async Task<Response<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            string id = _dataProtector.Unprotect(request.RoleId);
            //getting the Role object by RoleId
            Role roleToUpdate = await _roleRepository.GetRoleById(Convert.ToInt32(id));
            
            if(roleToUpdate != null)
            {
                bool response = await _roleRepository.UpdateRole(Convert.ToInt32(id), request.RoleName);
                if (response)
                {
                    return new Response<bool>() { Data = true, Message = "Role Updated Successfully", Succeeded = true };
                }
                return new Response<bool>() { Message = "Update Failed.", Succeeded = false };
            }
            return new Response<bool>() { Message = "Role Does Not Exist.", Succeeded = false };
        }
    }
}
