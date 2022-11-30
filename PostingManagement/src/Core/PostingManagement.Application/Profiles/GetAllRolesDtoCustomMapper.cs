using AutoMapper;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetAllRolesDtoCustomMapper : ITypeConverter<Role, GetAllRolesDto>
    {
        public GetAllRolesDto Convert(Role source, GetAllRolesDto destination, ResolutionContext context)
        {
            GetAllRolesDto dest = new GetAllRolesDto()
            {
                RoleId = EncryptionDecryption.EncryptString(source.RoleId.ToString()),
                RoleName = source.RoleName,
            };
            return dest;
        }
    }
}
