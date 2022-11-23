using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

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
            var userDetails = await _accountRepository.GetUserDetailsById(request.UId);
            if (userDetails != null)
            {
                bool response = await _accountRepository.UpdateUser(request.UId, request.UserName, request.RoleId, request.UpdatedBy);
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
