using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Queries.GetTransferList
{
    public class GetTransferListQueryHandler : IRequestHandler<GetTransferListQuery, Response<List<TransferListVM>>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public GetTransferListQueryHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }
        public async Task<Response<List<TransferListVM>>> Handle(GetTransferListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeTransferRepository.GetAllTransferListEmployees(request.PageNumber, request.NumberOfRecords);
            //checking Employee List is empty
            if (employees.Count != 0)
            {                
                return new Response<List<TransferListVM>>() { Data = employees, Succeeded = true };
            }
            else
            {
                return new Response<List<TransferListVM>>() { Message = "No Employee Exists", Succeeded = false };
            }
        }
    }
}
