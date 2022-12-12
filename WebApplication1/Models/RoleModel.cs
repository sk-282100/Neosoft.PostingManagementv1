using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models
{
    public class RoleModel
    {
        public string RoleId { get; set; }
        [MaxLength(15)]
        public string RoleName { get; set; }    
    }
}
