using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger
{
    public class GetAllTriggerQueryHandler : IRequestHandler<GetAllTriggerQuery, Response<List<GetAllTriggerDto>>>
    {
        private readonly ITriggerRepository _triggerRepository;
        private readonly IMapper _mapper;

        public GetAllTriggerQueryHandler(ITriggerRepository triggerRepository, IMapper mapper)
        {
             _mapper = mapper;
            _triggerRepository = triggerRepository;
        }
        public async Task<Response<List<GetAllTriggerDto>>> Handle(GetAllTriggerQuery request, CancellationToken cancellationToken)
        {
            var allTriggerList = await _triggerRepository.GetAllTriggerDetails();
            List<GetAllTriggerDto> result =_mapper.Map<List<GetAllTriggerDto>>(allTriggerList);

            return new Response<List<GetAllTriggerDto>>() { Succeeded = true ,Data = result };
        }
    }
}
