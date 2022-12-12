using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Queries.IsJobFamilyAlreadyExist
{
    public class IsJobFamilyAlreadyExistQueryHandler : IRequestHandler<IsJobFamilyAlreadyExistQuery, Response<bool>>
    {
        private readonly IJobFamilyRepository _jobFamilyRepository;
        public IsJobFamilyAlreadyExistQueryHandler(IJobFamilyRepository jobFamilyRepository)
        {
            _jobFamilyRepository = jobFamilyRepository; 
        }
        public async Task<Response<bool>> Handle(IsJobFamilyAlreadyExistQuery request, CancellationToken cancellationToken)
        {
            bool jobFamilyAlreadyExist = await _jobFamilyRepository.IsJobFamilyAlreadyExist(request.JobFamilyName);
            return new Response<bool>() { Data= jobFamilyAlreadyExist, Message="JobFamily Already Exist", Succeeded = true };
        }
    }
}
