using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.DeleteTrigger
{
    public class DeleteTriggerCommand : IRequest<Response<bool>>
    {
        public string TriggerId { get; set; }
    }
}
