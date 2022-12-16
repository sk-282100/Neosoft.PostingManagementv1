using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Queries.GetTransferList
{
    public class GetTransferListQueryHandler : IRequestHandler<GetTransferListQuery, Response<TransferListReponse>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public GetTransferListQueryHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }
        public async Task<Response<TransferListReponse>> Handle(GetTransferListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeTransferRepository.GetAllTransferListEmployees(request.PageNumber, request.NumberOfRecords);
            //checking Employee List is empty
            if (employees.TotalRecords != 0)
            {
                return new Response<TransferListReponse>() { Data = employees, Succeeded = true };
            }
            else
            {
                return new Response<TransferListReponse>() { Message = "No Employee Exists", Succeeded = false };
            }
        }
    }
}
