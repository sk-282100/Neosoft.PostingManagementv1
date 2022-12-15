namespace PostingManagement.UI.Models.Login
{
    public class SendOTPResponseModel
    {
        public bool SendEmailStatus { get; set; }
        public DateTime OTPExpiryTime { get; set; }
    }
}
