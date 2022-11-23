using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Response<List<UserDetailsDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;


        public GetAllUserQueryHandler(IAccountRepository accountRepository,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<UserDetailsDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var userDetailsList = await _accountRepository.GetAllUserDetails();
            List<UserDetailsDto> result = _mapper.Map<List<UserDetailsDto>>(userDetailsList);

            return new Response<List<UserDetailsDto>>() { Succeeded = true ,Data=result};
        }
    }
}
