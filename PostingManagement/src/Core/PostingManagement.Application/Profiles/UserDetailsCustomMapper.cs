using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class UserDetailsCustomMapper : ITypeConverter<UserDetails, UserDetailsDto>
    {
        private readonly IDataProtector _dataProtector;
        public UserDetailsCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public UserDetailsDto Convert(UserDetails source, UserDetailsDto destination, ResolutionContext context)
        {
            UserDetailsDto dto = new UserDetailsDto()
            {
                RoleId = _dataProtector.Protect(source.RoleId.ToString()),
                UId = _dataProtector.Protect(source.UId.ToString()),
                UserName = source.UserName,
                Email = source.Email
            };
            return dto;
        }
    }
}
