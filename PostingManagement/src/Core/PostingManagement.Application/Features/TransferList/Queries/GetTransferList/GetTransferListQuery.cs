using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.TransferList.Queries.GetTransferList
{
    public class GetTransferListQuery : IRequest<Response<TransferListReponse>>
    {
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public string Search { get; set; }
    }
}