using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Queries.GetMatchRequestTransferList
{
    public class GetMatchingRequestTransferListQuery:IRequest<Response<List<MatchingRequestTransferVacancy>>>
    {
        public List<int> EmployeeIdList { get; set; }   
    }
}
