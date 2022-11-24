using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Events.Commands.UpdateEvent;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;
        public EditRoleCommandHandler(IRoleRepository roleRepository,IMapper mapper,IDataProtectionProvider provider)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _protector = provider.CreateProtector("");
            
        }
        public async Task<Response<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            string id = _protector.Unprotect(request.RoleId);
           
            Role roleToUpdate = await _roleRepository.GetRoleById(Convert.ToInt32(id));
            roleToUpdate.RoleName=request.RoleName;
            //_mapper.Map(request, roleToUpdate, typeof(EditRoleCommand), typeof(Role));
            await _roleRepository.UpdateAsync(roleToUpdate);
            return new Response<bool>() { Data = true, Message = "Role Updated Successfully", Succeeded = true };
        }
    }
}
