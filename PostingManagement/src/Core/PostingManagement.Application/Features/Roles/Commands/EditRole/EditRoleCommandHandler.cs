using AutoMapper;
using MediatR;
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
        public EditRoleCommandHandler(IRoleRepository roleRepository,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            Role roleToUpdate = await _roleRepository.GetRoleById(request.RoleId);
            _mapper.Map(request, roleToUpdate, typeof(EditRoleCommand), typeof(Role));
            await _roleRepository.UpdateAsync(roleToUpdate);
            return new Response<bool>() { Data = true, Message = "Role Updated Successfully", Succeeded = true };
        }
    }
}
