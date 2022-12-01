using AutoMapper;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetRolesByIdCustomMapper : ITypeConverter<Role, GetRoleByIdDto>
    {
        public GetRoleByIdDto Convert(Role source, GetRoleByIdDto destination, ResolutionContext context)
        {
            GetRoleByIdDto dest = new GetRoleByIdDto()
            {
                RoleId = EncryptionDecryption.EncryptString(source.RoleId.ToString()),
                RoleName = source.RoleName,
            };

            return dest;
        }
    }
}
