using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById
{
    public class GetJobFamilyByIdQueryHandler : IRequestHandler<GetJobFamilyByIdQuery, Response<GetjobFamilyByIdDto>>
    {
        private readonly IJobFamilyRepository _jobFamilyRepository;
        private readonly IDataProtector _dataProtector;
        private readonly IMapper _mapper;
        public GetJobFamilyByIdQueryHandler(IJobFamilyRepository jobFamilyRepository,IMapper mapper,IDataProtectionProvider provider)
        {
            _jobFamilyRepository = jobFamilyRepository;
            _mapper = mapper;
            _dataProtector = provider.CreateProtector("");
        }
        public async Task<Response<GetjobFamilyByIdDto>> Handle(GetJobFamilyByIdQuery request, CancellationToken cancellationToken)
        {
            string id = _dataProtector.Unprotect(request.JobFamilyId);
            JobFamilies jobFamilies = await _jobFamilyRepository.GetJobFamilyById(Convert.ToInt32(id));
            if(jobFamilies != null)
            {
                var JobFamilyDto = _mapper.Map<GetjobFamilyByIdDto>(jobFamilies);
                return new Response<GetjobFamilyByIdDto> { Data = JobFamilyDto, Succeeded = true };

            }
            else
            {
                return new Response<GetjobFamilyByIdDto> { Message = "JobFamily Does Not Exist", Succeeded = false };
            }
        }
    }
}
