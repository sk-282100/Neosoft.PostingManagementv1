using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response<bool>>
    {
        private IRoleRepository _roleRepository;    
       
        private IDataProtector _protector;
        public DeleteRoleCommandHandler(IRoleRepository roleRepository,IDataProtectionProvider provider)
        {
            _roleRepository = roleRepository;
            _protector = provider.CreateProtector(""); 
               
        }
        public async Task<Response<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            string id = _protector.Unprotect(request.RoleId);
            Role role = await _roleRepository.GetRoleById(Convert.ToInt32(id));
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
