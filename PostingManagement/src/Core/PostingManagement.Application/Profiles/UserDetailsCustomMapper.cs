using AutoMapper;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class UserDetailsCustomMapper : ITypeConverter<UserDetails, UserDetailsDto>
    {
        public UserDetailsDto Convert(UserDetails source, UserDetailsDto destination, ResolutionContext context)
        {
            UserDetailsDto dto = new UserDetailsDto()
            {
                RoleId = EncryptionDecryption.EncryptString(source.RoleId.ToString()),
                UId = EncryptionDecryption.EncryptString(source.UId.ToString()),
                UserName = source.UserName
                
            };
            return dto;
        }
    }
}
