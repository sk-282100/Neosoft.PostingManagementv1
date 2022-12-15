namespace PostingManagement.Application.Features.LogIn.Queries.SendOTP
{
    public class SendOTPDto
    {
        public bool SendEmailStatus { get; set; }
        public DateTime OTPExpiryTime { get; set; }
    }
}
