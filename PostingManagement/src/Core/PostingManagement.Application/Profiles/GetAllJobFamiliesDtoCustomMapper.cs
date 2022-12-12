using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Profiles
{
    public class GetAllJobFamiliesDtoCustomMapper : ITypeConverter<JobFamilies, GetAllJobFamilyDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetAllJobFamiliesDtoCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public GetAllJobFamilyDto Convert(JobFamilies source, GetAllJobFamilyDto destination, ResolutionContext context)
        {
            GetAllJobFamilyDto result = new GetAllJobFamilyDto()
            {
                JobFamilyId = _dataProtector.Protect(source.JobFamilyId.ToString()),
                JobFamilyName = source.JobFamilyName,   
            };
            return result;
        }
    }
}
