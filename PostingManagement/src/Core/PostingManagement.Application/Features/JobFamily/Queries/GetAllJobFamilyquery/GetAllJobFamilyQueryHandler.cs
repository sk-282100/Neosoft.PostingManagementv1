using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery
{
    public class GetAllJobFamilyQueryHandler : IRequestHandler<GetAllJobFamilyQuery, Response<List<GetAllJobFamilyDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IJobFamilyRepository _jobFamilyRepository;
        public GetAllJobFamilyQueryHandler(IMapper mapper, IJobFamilyRepository jobFamilyRepository)
        {
            _mapper = mapper;
            _jobFamilyRepository = jobFamilyRepository;
        }
        public async Task<Response<List<GetAllJobFamilyDto>>> Handle(GetAllJobFamilyQuery request, CancellationToken cancellationToken)
        {
            var jobFamilies = await _jobFamilyRepository.GetAllJobFamily();
            if (jobFamilies.Count != 0)
            {
                List<GetAllJobFamilyDto> jobFamilyDtos = _mapper.Map<List<GetAllJobFamilyDto>>(jobFamilies);
                return new Response<List<GetAllJobFamilyDto>>() { Data = jobFamilyDtos, Succeeded = true };
            }
            else
            {
                return new Response<List<GetAllJobFamilyDto>>() { Message = "JobFamily Does Not Exist", Succeeded = false };
            }
        }
    }
}
