using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Commands.InsertIntoTransferListForZO
{
    public class TransferListForZOCommandHandler : IRequestHandler<TransferListForZOCommand, Response<ZOTransferListReponse>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public TransferListForZOCommandHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }
        public async Task<Response<ZOTransferListReponse>> Handle(TransferListForZOCommand request, CancellationToken cancellationToken)
        {
            var response = await _employeeTransferRepository.InsertIntoTransferListForZo(request.TransferList);
            if(response.SuccessCount > 0)
            {
                return new Response<ZOTransferListReponse>() { Data = response, Succeeded = true };
            }
            else
            {
                return new Response<ZOTransferListReponse>() { Succeeded = false, Message = "Insertion Failed" };
            }
        }        
    }
}
