using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetAllUserDetailsCustomMapper:ITypeConverter<UserDetailsVm,GetAllUserDetailsDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetAllUserDetailsCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public GetAllUserDetailsDto Convert(UserDetailsVm source, GetAllUserDetailsDto destination, ResolutionContext context)
        {

            GetAllUserDetailsDto dto = new GetAllUserDetailsDto()
            {
                UId = _dataProtector.Protect(source.UId.ToString()),
                UserName = source.UserName,
                Role = source.Role,
                Email = source.Email
            };
            return dto;
        }
    }
}
