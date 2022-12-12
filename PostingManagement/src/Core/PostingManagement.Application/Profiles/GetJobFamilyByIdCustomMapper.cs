using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Profiles
{
    public class GetJobFamilyByIdCustomMapper : ITypeConverter<JobFamilies, GetjobFamilyByIdDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetJobFamilyByIdCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public GetjobFamilyByIdDto Convert(JobFamilies source, GetjobFamilyByIdDto destination, ResolutionContext context)
        {
            GetjobFamilyByIdDto dest = new GetjobFamilyByIdDto()
            {
                JobFamilyId = _dataProtector.Protect(source.JobFamilyId.ToString()),
                JobFamilyName = source.JobFamilyName,
            };

            return dest;
        }
    }
}
