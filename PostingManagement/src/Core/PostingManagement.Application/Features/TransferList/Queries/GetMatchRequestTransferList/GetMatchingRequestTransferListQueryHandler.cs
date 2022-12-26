using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Queries.GetMatchRequestTransferList
{
    public class GetMatchingRequestTransferListQueryHandler : IRequestHandler<GetMatchingRequestTransferListQuery, Response<List<MatchingRequestTransferVacancy>>>
    {
        private readonly IEmployeeTransferRepository _employeeTransferRepository;
        public GetMatchingRequestTransferListQueryHandler(IEmployeeTransferRepository employeeTransferRepository)
        {
            _employeeTransferRepository = employeeTransferRepository;
        }

        public async Task<Response<List<MatchingRequestTransferVacancy>>> Handle(GetMatchingRequestTransferListQuery request, CancellationToken cancellationToken)
        {
            var matchingEmployeesVacancy =  await _employeeTransferRepository.GetMatchingRequestTransferList(request.EmployeeIdList);
            return new Response<List<MatchingRequestTransferVacancy>>() { Data = matchingEmployeesVacancy,Errors = null };
        }
    }
}
