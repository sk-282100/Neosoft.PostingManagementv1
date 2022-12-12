using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.LogIn.Commands
{
    public class VerifyOTPCommand : IRequest<Response<bool>>
    {
        public string Username { get; set; }
        public int OTP { get; set; }
        public DateTime OTPSubmitionTime { get; set; } 
    }
}
