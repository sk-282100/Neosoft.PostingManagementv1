using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger
{
    public class UpdateTriggerCommand : IRequest<Response<bool>>
    {
        public string TriggerId { get; set; }
        public string ScaleId { get; set; }
        public int Tenure { get; set; }
        public string Mandatory { get; set; }
    }
}
