using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostingManagement.Domain.Entities
{
    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string RoleId { get; set; }  
        public string RoleName { get; set; }

    }
}
