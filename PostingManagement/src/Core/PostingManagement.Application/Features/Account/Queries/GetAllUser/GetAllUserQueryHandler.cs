using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Response<List<GetAllUserDetailsDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IAccountRepository accountRepository,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetAllUserDetailsDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var userDetailsList = await _accountRepository.GetAllUserDetails();
            List<GetAllUserDetailsDto> result = _mapper.Map<List<GetAllUserDetailsDto>>(userDetailsList);

            return new Response<List<GetAllUserDetailsDto>>() { Succeeded = true ,Data=result};
        }
    }
}
