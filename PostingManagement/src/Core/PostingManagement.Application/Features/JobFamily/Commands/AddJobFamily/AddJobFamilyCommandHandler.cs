using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Commands.AddJobFamily
{
    public class AddJobFamilyCommandHandler : IRequestHandler<AddjobFamilyCommand, Response<bool>>
    {
        private readonly IJobFamilyRepository _jobFamilyRepository;
        public AddJobFamilyCommandHandler(IJobFamilyRepository jobFamilyRepository)
        {
            _jobFamilyRepository = jobFamilyRepository; 
        }
        public async Task<Response<bool>> Handle(AddjobFamilyCommand request, CancellationToken cancellationToken)
        {
            JobFamilies jobFamily = await _jobFamilyRepository.GetJobFamilyByName(request.JobFamilyName);
            if(jobFamily == null)
            {
                bool result = await _jobFamilyRepository.AddAsync(request.JobFamilyName);
                return new Response<bool>() { Data = result, Message = "Job Family Added Successfully", Succeeded = true };
            }
            else
            {
                return new Response<bool>() { Data = false,Message = "Job Family Already Exist", Succeeded = false };   
            }
        }
    }
}
