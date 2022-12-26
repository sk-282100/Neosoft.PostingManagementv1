using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Queries.GetTriggerById
{
    public class GetTriggerByIdQuery : IRequest<Response<GetTriggerByIdDto>>
    {
        public string TriggerId { get; set; }
    }
}
