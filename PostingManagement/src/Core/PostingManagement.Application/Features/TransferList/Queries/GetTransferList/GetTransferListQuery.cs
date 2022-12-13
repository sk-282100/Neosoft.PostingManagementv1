using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Queries.GetTransferList
{
    public class GetTransferListQuery : IRequest<Response<List<TransferListVM>>>
    {
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
