using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Profiles
{
    public class GetRolesByIdCustomMapper : ITypeConverter<Role, GetRoleByIdDto>
    {
        private readonly IDataProtector _protector;
        public GetRolesByIdCustomMapper(IDataProtectionProvider provider)
        {

            _protector = provider.CreateProtector("");
        }
        public GetRoleByIdDto Convert(Role source, GetRoleByIdDto destination, ResolutionContext context)
        {
            GetRoleByIdDto dest = new GetRoleByIdDto()
            {
                RoleId = _protector.Protect(source.RoleId.ToString()),
                RoleName = source.RoleName,

            };

            return dest;
        }
    }
}
