using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;

        public ResetPasswordCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Response<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var isUserNameExist = await _accountRepository.IsUserNamePresent(request.UserName);
            if (isUserNameExist)
            {
                var resetStatus = await _accountRepository.ResetPassword(request.UserName, request.NewPassword);
                return new Response<bool>() { Succeeded = true, Data = resetStatus ,Message="Password is Updated Successfully"};
            }
            else
            {
                return new Response<bool> { Succeeded = false, Data = false ,Message="Username is not Exist" };
            }
        }
    }
}
