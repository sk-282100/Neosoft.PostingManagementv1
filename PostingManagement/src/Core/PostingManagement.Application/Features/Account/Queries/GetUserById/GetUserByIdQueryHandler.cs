using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserDetailsDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserDetailsDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //Decrypting the UId 
            int id = Convert.ToInt32(EncryptionDecryption.DecryptString(request.UId));

            UserDetails userDetails = await _accountRepository.GetUserDetailsById(id);
            UserDetailsDto response = _mapper.Map<UserDetailsDto>(userDetails);

            if (response.UserName == null)
            {
                return new Response<UserDetailsDto>() { Succeeded = false, Data = response, Message = "User Not Found " };
            }
            else if (response.UserName != null)
            {
                return new Response<UserDetailsDto>() { Succeeded = true, Data = response };
            }
            return new Response<UserDetailsDto> { Succeeded = false, Data = response };
        }
    }
}
