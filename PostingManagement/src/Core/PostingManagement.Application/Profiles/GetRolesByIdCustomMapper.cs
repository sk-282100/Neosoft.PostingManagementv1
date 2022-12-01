using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetRolesByIdCustomMapper : ITypeConverter<Role, GetRoleByIdDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetRolesByIdCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }

        public GetRoleByIdDto Convert(Role source, GetRoleByIdDto destination, ResolutionContext context)
        {
            GetRoleByIdDto dest = new GetRoleByIdDto()
            {
                RoleId = _dataProtector.Protect(source.RoleId.ToString()),
                RoleName = source.RoleName,
            };

            return dest;
        }
    }
}
