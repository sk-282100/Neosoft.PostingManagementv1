using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Profiles
{
    public class GetAllUserDetailsCustomMapper:ITypeConverter<UserDetailsVm,GetAllUserDetailsDto>
    {
        private readonly IDataProtector _protector;
        public GetAllUserDetailsCustomMapper(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("");
        }

       

        public GetAllUserDetailsDto Convert(UserDetailsVm source, GetAllUserDetailsDto destination, ResolutionContext context)
        {

            GetAllUserDetailsDto dto = new GetAllUserDetailsDto()
            {
                UId = _protector.Protect(source.UId.ToString()),
                UserName = source.UserName,
                Role = source.Role
            };
            return dto;
        }
    }
}
