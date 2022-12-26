using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Scales.Queries.GetAllScales
{
    public class GetAllScaleQueryHandler : IRequestHandler<GetAllScalesQuery , Response<List<ScaleDto>>>
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IMapper _mapper;
        public GetAllScaleQueryHandler(IScaleRepository scaleRepository,IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<ScaleDto>>> Handle(GetAllScalesQuery request, CancellationToken cancellationToken)
        {
            var scaleList = await _scaleRepository.GetAllScaleDetails();
            List<ScaleDto> result = _mapper.Map<List<ScaleDto>>(scaleList);
            return new Response<List<ScaleDto>>() { Succeeded = true, Data = result };
        }
    }
}
