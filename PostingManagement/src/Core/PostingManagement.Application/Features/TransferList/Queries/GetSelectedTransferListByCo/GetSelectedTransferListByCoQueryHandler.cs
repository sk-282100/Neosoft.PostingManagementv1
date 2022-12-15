using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Queries.GetSelectedTransferListByCo
{
    public class GetSelectedTransferListByCoQueryHandler : IRequestHandler<GetSelectedTransferListByCoQuery, Response<List<TransferListVM>>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public GetSelectedTransferListByCoQueryHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }

        public async Task<Response<List<TransferListVM>>> Handle(GetSelectedTransferListByCoQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeTransferRepository.GetTransferListEmployeesByEmployeeId(request.EmployeeIdList);
            if (employees.Count != 0)
            {
                return new Response<List<TransferListVM>>() { Data = employees, Succeeded = true };
            }
            else
            {
                return new Response<List<TransferListVM>>() { Message = "No Employee Selected", Succeeded = false };
            }
        }


    }
}
