using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models.Login
{
    public class VerifyOTPRequestModel
    {
        public string Username { get; set; }
       
        public int OTP { get; set; }
        public DateTime OTPSubmitionTime { get; set; }
    }
}
