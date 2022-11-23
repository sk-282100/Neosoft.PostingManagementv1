using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;

        public AddUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;   
        }

        public async Task<Response<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            bool result = await _accountRepository.AddUser(request.UserName, request.RoleId, request.CreatedBy);
            return new Response<bool>() { Succeeded = true, Data = result };
        }
    }
}
