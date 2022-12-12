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

namespace PostingManagement.Application.Features.JobFamily.Commands.EditJobFamily
{
    public class EditJobFamilyCommandHandler : IRequestHandler<EditJobFamilyCommand, Response<bool>>
    {
        private readonly IJobFamilyRepository _jobFamilyRepository;
        private readonly IDataProtector _dataProtector;
        public EditJobFamilyCommandHandler(IJobFamilyRepository jobFamilyRepository,IDataProtectionProvider provider)
        {
            _jobFamilyRepository = jobFamilyRepository;
            _dataProtector = provider.CreateProtector("");
        }
        public async Task<Response<bool>> Handle(EditJobFamilyCommand request, CancellationToken cancellationToken)
        {
            string id = _dataProtector.Unprotect(request.JobFamilyId);
            JobFamilies jobFamilytoUpdate = await _jobFamilyRepository.GetJobFamilyById(Convert.ToInt32(id));
            jobFamilytoUpdate.JobFamilyName = request.JobFamilyName;    
            await _jobFamilyRepository.UpdateAsync(jobFamilytoUpdate);
            return new Response<bool> { Data = true,Message = "JobFamily Updated Successfully",Succeeded = true };
        }
    }
}
