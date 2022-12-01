using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Account.Command.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommad, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;

        public EditUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Response<bool>> Handle(EditUserCommad request, CancellationToken cancellationToken)
        {
            //Decrypting the UId and RoleId
            int id = Convert.ToInt32(EncryptionDecryption.DecryptString(request.UId));
            int roleId = Convert.ToInt32(EncryptionDecryption.DecryptString(request.RoleId));

            var userDetails = await _accountRepository.GetUserDetailsById(id);

            if (userDetails != null)
            {
                bool response = await _accountRepository.UpdateUser(id, request.UserName,roleId, request.UpdatedBy);
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
