using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Events.Queries.GetEventsList;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Profiles
{
    public class GetAllRolesDtoCustomMapper : ITypeConverter<Role, GetAllRolesDto>
    {
        private readonly IDataProtector _protector;

        public GetAllRolesDtoCustomMapper(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("");
        }
        public GetAllRolesDto Convert(Role source, GetAllRolesDto destination, ResolutionContext context)
        {
            GetAllRolesDto dest = new GetAllRolesDto()
            {
                RoleId = _protector.Protect(source.RoleId.ToString()),
                RoleName = source.RoleName,
               
            };

            return dest;
        }
    }
}
