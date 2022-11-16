namespace PostingManagement.UI.Models
{
    public class LoginResponseModel
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
