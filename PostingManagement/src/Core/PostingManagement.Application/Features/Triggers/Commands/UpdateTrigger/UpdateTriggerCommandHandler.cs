using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger
{
    public class UpdateTriggerCommandHandler : IRequestHandler<UpdateTriggerCommand, Response<bool>>
    {
        private readonly ITriggerRepository _repository;
        private readonly IDataProtector _dataProtector;
        public UpdateTriggerCommandHandler(ITriggerRepository repository, IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(UpdateTriggerCommand request, CancellationToken cancellationToken)
        {
            //Decrypting the TriggerID and ScaleId
            int triggerId = Convert.ToInt32(_dataProtector.Unprotect(request.TriggerId));
            int scaleId = Convert.ToInt32(_dataProtector.Unprotect(request.ScaleId));

            var userDetails = await _repository.GetTriggerDetailsById(triggerId);

            if (userDetails != null)
            {
                bool response = await _repository.UpdateTrigger(triggerId,scaleId,request.Tenure,request.Mandatory);
                if (response)
                {
                    return new Response<bool>() { Succeeded = true, Data = response, Message = "Requested Trigger Update successfully " };
                }
                return new Response<bool>() { Succeeded = false, Data = response, Message = "Updation Failed !!!" };
            }
            return new Response<bool>() { Succeeded = false, Message = "This Trigger is not Present" };
        }
    }
}
