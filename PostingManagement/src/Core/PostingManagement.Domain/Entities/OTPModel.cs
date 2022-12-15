using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class OTPModel
    {
        [Key]
        public int UId { get; set; }
        public string Email { get; set; }
        public int OTP { get; set; }
        public DateTime ExpiryTime { get; set; } 
    }
}
