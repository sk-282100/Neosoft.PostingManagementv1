using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetAllRolesDtoCustomMapper : ITypeConverter<Role, GetAllRolesDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetAllRolesDtoCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }

        public GetAllRolesDto Convert(Role source, GetAllRolesDto destination, ResolutionContext context)
        {
            GetAllRolesDto dest = new GetAllRolesDto()
            {
                RoleId = _dataProtector.Protect(source.RoleId.ToString()),
                RoleName = source.RoleName,
            };
            return dest;
        }
    }
}
