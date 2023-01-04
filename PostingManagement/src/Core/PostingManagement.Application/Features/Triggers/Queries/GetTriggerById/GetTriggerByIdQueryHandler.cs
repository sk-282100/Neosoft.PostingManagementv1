using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.Triggers.Queries.GetTriggerById
{
    public class GetTriggerByIdQueryHandler : IRequestHandler<GetTriggerByIdQuery, Response<GetTriggerByIdDto>>
    {
        private readonly ITriggerRepository _triggerRepository;
        private readonly IMapper _mapper;
        private readonly IDataProtector _dataProtector;
        public GetTriggerByIdQueryHandler(ITriggerRepository triggerRepository, IMapper mapper, IDataProtectionProvider provider)
        {
            _triggerRepository = triggerRepository;
            _mapper = mapper;
            _dataProtector = provider.CreateProtector("");
        }

        public async Task<Response<GetTriggerByIdDto>> Handle(GetTriggerByIdQuery request, CancellationToken cancellationToken)
        {
            //Decrypting the TriggerId 
            int id = Convert.ToInt32(_dataProtector.Unprotect(request.TriggerId));

            Trigger trigger = await _triggerRepository.GetTriggerDetailsById(id);
            var response = _mapper.Map<GetTriggerByIdDto>(trigger);

            if (trigger == null)
            {
                return new Response<GetTriggerByIdDto>() { Succeeded = false, Data = response, Message = "Trigger Not Found " };
            }
            else if (trigger != null)
            {
                return new Response<GetTriggerByIdDto>() { Succeeded = true, Data = response };
            }
            return new Response<GetTriggerByIdDto> { Succeeded = false, Data = response };
        }
    }
}
