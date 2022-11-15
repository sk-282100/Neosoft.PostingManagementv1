using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models
{
    public class LoginModel
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
