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

namespace PostingManagement.Application.Features.JobFamily.Commands.DeleteJobFamily
{
    public class DeleteJobFamilyCommandHandler : IRequestHandler<DeleteJobFamilyCommand, Response<bool>>
    {
        private readonly IJobFamilyRepository _jobFamilyRepository;
        private readonly IDataProtector _dataProtector;
        public DeleteJobFamilyCommandHandler(IJobFamilyRepository jobFamilyRepository,IDataProtectionProvider provider)
        {
            _jobFamilyRepository = jobFamilyRepository;
            _dataProtector = provider.CreateProtector("");
        }
        public async Task<Response<bool>> Handle(DeleteJobFamilyCommand request, CancellationToken cancellationToken)
        {
            string id = _dataProtector.Unprotect(request.JobFamilyId);
            JobFamilies jobFamilies = await _jobFamilyRepository.GetJobFamilyById(Convert.ToInt32(id));
            if(jobFamilies != null)
            {
                bool result = await _jobFamilyRepository.DeleteAsync(jobFamilies);
                return new Response<bool>() { Data = result, Message = "JobFamily Deleted Successfully", Succeeded = true };
            }
            else
            {
                return new Response<bool>() { Data = false, Message = "JobFamily Does Not Exist",Succeeded = false };
            }
        }
    }
}
