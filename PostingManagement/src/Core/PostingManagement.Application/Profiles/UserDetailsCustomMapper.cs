using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Profiles
{
    public class UserDetailsCustomMapper : ITypeConverter<UserDetails, UserDetailsDto>
    {
        private readonly IDataProtector _protector;
        public UserDetailsCustomMapper(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("");
        }

        public UserDetailsDto Convert(UserDetails source, UserDetailsDto destination, ResolutionContext context)
        {
            UserDetailsDto dto = new UserDetailsDto()
            {
                UId = _protector.Protect(source.UId.ToString()),
                UserName = source.UserName,
                RoleId = source.RoleId
            };
            return dto;
        }
    }
}
