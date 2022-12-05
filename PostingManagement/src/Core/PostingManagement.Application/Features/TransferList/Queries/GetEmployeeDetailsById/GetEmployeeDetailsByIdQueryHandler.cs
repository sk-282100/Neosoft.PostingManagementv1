using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById
{
    public class GetEmployeeDetailsByIdQueryHandler : IRequestHandler<GetEmployeeDetailsByIdQuery, Response<EmployeeDetailsForTransferList>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public GetEmployeeDetailsByIdQueryHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }
        public async Task<Response<EmployeeDetailsForTransferList>> Handle(GetEmployeeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeTransferRepository.GetEmployeeByIdAndMovementType(request.EmployeeId, request.MovementType);
            //checking Employee List is empty
            if (employee != null)
            {
                return new Response<EmployeeDetailsForTransferList> { Data = employee, Succeeded = true };
            }
            else
            {
                return new Response<EmployeeDetailsForTransferList>() { Data = employee, Message = "Employee Does Not Exist", Succeeded = false };
            }
        }
    }
}
