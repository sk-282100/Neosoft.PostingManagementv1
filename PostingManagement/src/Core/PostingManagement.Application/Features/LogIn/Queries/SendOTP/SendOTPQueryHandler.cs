using MediatR;
using PostingManagement.Application.Contracts.Infrastructure;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Models.Mail;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Features.LogIn.Queries.SendOTP
{
    public class SendOTPQueryHandler : IRequestHandler<SendOTPQuery, Response<SendOTPDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IEmailService _emailService;
        private readonly ISMTPEmailService _emailService1;
        public SendOTPQueryHandler(IAccountRepository accountRepository, ILoginRepository loginRepository,IEmailService emailService, ISMTPEmailService emailService1)
        {
            _accountRepository = accountRepository;
            _loginRepository = loginRepository;
            _emailService = emailService;
            _emailService1 = emailService1;
        }

        public async Task<Response<SendOTPDto>> Handle(SendOTPQuery request, CancellationToken cancellationToken)
        {
            bool isUsernamePresent = await _accountRepository.IsUserNamePresent(request.Username);
            if (isUsernamePresent)
            {
                var userDetails = await _accountRepository.GetUserByUserName(request.Username);
                int otp = await _loginRepository.generateOTP(4); // generate 4-digit OTP
                DateTime expiryTime = DateTime.Now.AddMinutes(5);// expirytime for OTP 

                //Sending OTP to User's email

                try
                {
                    
                    bool emailStatus = await _emailService1.SendOTPEmail(userDetails.Email, otp, expiryTime); 
                    if(emailStatus == true)
                    {
                        OTPModel otpModel = new OTPModel()
                        {
                            UId=userDetails.UId,
                            Email = userDetails.Email,
                            OTP = otp,
                            ExpiryTime = expiryTime
                        };
                        await _loginRepository.SaveOTP(otpModel);
                        SendOTPDto response = new() { SendEmailStatus = emailStatus, OTPExpiryTime = expiryTime };
                        return new Response<SendOTPDto>() { Succeeded = true, Message = "OTP sent to Email Successfully", Data = response };
                    }
                    else
                    {
                        return new Response<SendOTPDto>() { Succeeded = false, Message = "User Email is not working Properly , contact to administration for email change", Data = null };

                    }
                }
                catch
                {
                    return new Response<SendOTPDto>() { Succeeded = false, Message = "Email Service is not working now , try again later", Data = null };

                }

            }
            else
            {
                return new Response<SendOTPDto>() { Succeeded = false, Message = "User is Not Found", Data = null }; 
            }
        }
    }
}
