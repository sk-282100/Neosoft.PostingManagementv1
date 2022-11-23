using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<GetUserByIdDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IAccountRepository accountRepository,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserDetails userDetails = await _accountRepository.GetUserDetailsById(request.UId);
            GetUserByIdDto response = _mapper.Map<GetUserByIdDto>(userDetails);
             if(response.UserName == null)
            {
                return new Response<GetUserByIdDto>() { Succeeded = false, Data = response, Message = "User Not Found " };
            }
             else if(response.UserName != null)
            {
                return new Response<GetUserByIdDto>() { Succeeded = true, Data = response };
            }
             return new Response<GetUserByIdDto> { Succeeded = false ,Data=response};
        }
    }
}
