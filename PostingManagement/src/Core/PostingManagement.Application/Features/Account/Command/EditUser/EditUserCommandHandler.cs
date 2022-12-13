using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommad, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDataProtector _dataProtector;
        public EditUserCommandHandler(IAccountRepository accountRepository,IDataProtectionProvider provider)
        {
            _accountRepository = accountRepository;
            _dataProtector = provider.CreateProtector("");
        }

        public async Task<Response<bool>> Handle(EditUserCommad request, CancellationToken cancellationToken)
        {
            //Decrypting the UId and RoleId
            int id = Convert.ToInt32(_dataProtector.Unprotect(request.UId));
            int roleId = Convert.ToInt32(_dataProtector.Unprotect(request.RoleId));

            var userDetails = await _accountRepository.GetUserDetailsById(id);

            if (userDetails != null)
            {
                bool response = await _accountRepository.UpdateUser(id, request.UserName,roleId,request.Email, request.UpdatedBy);
                if (response)
                {
                    return new Response<bool>() { Succeeded = true, Data = response, Message = "Requested User Update successfully " };
                }
                return new Response<bool>() { Succeeded = false, Data = response, Message = "Updation Failed !!!" };
            }
            return new Response<bool>() { Succeeded = false, Message = "User not Found" };
        }
    }
}
