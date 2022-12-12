using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.LogIn.Commands
{
    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, Response<bool>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILoginRepository _loginRepository;
        public VerifyOTPCommandHandler(IAccountRepository accountRepository, ILoginRepository loginRepository)
        {
            _accountRepository = accountRepository;
            _loginRepository = loginRepository;
        }

        public async Task<Response<bool>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var userDetails = await _accountRepository.GetUserByUserName(request.Username);
            if(userDetails != null)
            {
                var dbOTPDetails = await _loginRepository.GetOTPDetailsByUId(userDetails.UId);
                //check otp details are present
                if (dbOTPDetails == null) 
                {
                    return new Response<bool>() { Succeeded = false, Message = "Please generate OTP First" };
                }

                //Check Expiry time 
                if(dbOTPDetails.ExpiryTime >= request.OTPSubmitionTime)
                {
                    //Verify the OTP 
                    if (dbOTPDetails.OTP == request.OTP)
                    {
                        return new Response<bool>() { Succeeded = true, Message = "OTP Verified Successfully", Data = true };
                    }
                    else 
                    {
                        return new Response<bool>() { Succeeded = true, Message = "OTP didn't Match , Please try again", Data = false };
                    }
                }
                else
                {
                    return new Response<bool>() { Succeeded = true, Message = "OTP has expired", Data = false };
                }
            }
            else
            {
                return new Response<bool>() { Succeeded = false, Message = "User is not Found" };
            }
        }
    }
}
