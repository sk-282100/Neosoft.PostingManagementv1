using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Queries.IsUserNamePresent
{
    public class IsUserNamePresentQueryHandler : IRequestHandler<IsUserNamePresentQuery, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;
        public IsUserNamePresentQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Response<bool>> Handle(IsUserNamePresentQuery request, CancellationToken cancellationToken)
        {
            bool result = await _accountRepository.IsUserNamePresent(request.UserName);

            return new Response<bool>() { Succeeded = true, Data = result };    
        }
    }
}
