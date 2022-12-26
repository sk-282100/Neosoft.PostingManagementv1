using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Scales.Queries.GetAllScales
{
    public class GetAllScalesQuery : IRequest<Response<List<ScaleDto>>>
    {
    }
}
