using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models.AccountModels
{
    public class CreateUserRequestModel
    {
        [MaxLength(15)]
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string CreatedBy { get; set; }
    }
}
