using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Account.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;
        public AddUserCommandHandler(IAccountRepository accountRepository,IDataProtectionProvider provider)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Response<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            int roleId = Convert.ToInt32(EncryptionDecryption.DecryptString(request.RoleId));
            bool result = await _accountRepository.AddUser(request.UserName,roleId, request.CreatedBy);
            return new Response<bool>() { Succeeded = true, Data = result };
        }
    }
}
