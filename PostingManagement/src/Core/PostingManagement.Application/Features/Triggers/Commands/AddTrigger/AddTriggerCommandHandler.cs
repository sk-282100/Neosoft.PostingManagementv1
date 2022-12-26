using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.AddTrigger
{
    public class AddTriggerCommandHandler : IRequestHandler<AddTriggerCommand, Response<bool>>
    {
        private readonly ITriggerRepository _triggerRepository;
        private readonly IDataProtector _dataProtector;

        public AddTriggerCommandHandler(ITriggerRepository triggerRepository, IDataProtectionProvider provider)
        {
            _triggerRepository = triggerRepository;
            _dataProtector = provider.CreateProtector("");
        }

        public async Task<Response<bool>> Handle(AddTriggerCommand request, CancellationToken cancellationToken)
        {
            int scaleId = Convert.ToInt32(_dataProtector.Unprotect(request.ScaleId));
            var result = await _triggerRepository.AddTrigger(scaleId, request.Tenure, request.Mandatory);
            return new Response<bool>() { Succeeded = true, Data = result };
        }
    }
}
