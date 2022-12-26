using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger
{
    public class GetAllTriggerQuery : IRequest<Response<List<GetAllTriggerDto>>>
    {
    }
}
