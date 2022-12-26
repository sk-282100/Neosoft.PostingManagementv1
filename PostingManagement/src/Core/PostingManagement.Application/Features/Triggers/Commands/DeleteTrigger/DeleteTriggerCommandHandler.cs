using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.DeleteTrigger
{
    public class DeleteTriggerCommandHandler : IRequestHandler<DeleteTriggerCommand, Response<bool>>
    {
        private readonly ITriggerRepository _repository;
        private readonly IDataProtector _dataProtector;
        public DeleteTriggerCommandHandler(ITriggerRepository repository, IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(DeleteTriggerCommand request, CancellationToken cancellationToken)
        {
            //Decrypting the TriggerId
            int id = Convert.ToInt32(_dataProtector.Unprotect(request.TriggerId));
            bool response = await _repository.DeleteTrigger(id);

            //check the deletion status
            if (response)
            {
                return new Response<bool>() { Succeeded = true, Data = response, Message = "Requested Trigger Deleted successfully " };
            }

            return new Response<bool>() { Succeeded = false, Data = response, Message = "Deletion Failed !!!" };
        }
    }
}
