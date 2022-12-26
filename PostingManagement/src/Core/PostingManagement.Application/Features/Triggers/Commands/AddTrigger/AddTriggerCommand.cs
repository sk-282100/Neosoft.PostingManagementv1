using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Commands.AddTrigger
{
    public class AddTriggerCommand : IRequest<Response<bool>>
    {
        public string ScaleId { get; set; }
        public int Tenure { get; set; }
        public string Mandatory { get; set; }
    }
}
