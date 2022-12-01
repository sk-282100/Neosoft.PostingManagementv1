using AutoMapper;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Profiles
{
    public class GetAllUserDetailsCustomMapper:ITypeConverter<UserDetailsVm,GetAllUserDetailsDto>
    {
       
        public GetAllUserDetailsDto Convert(UserDetailsVm source, GetAllUserDetailsDto destination, ResolutionContext context)
        {

            GetAllUserDetailsDto dto = new GetAllUserDetailsDto()
            {
                UId = EncryptionDecryption.EncryptString(source.UId.ToString()),
                UserName = source.UserName,
                Role = source.Role
            };
            return dto;
        }
    }
}
