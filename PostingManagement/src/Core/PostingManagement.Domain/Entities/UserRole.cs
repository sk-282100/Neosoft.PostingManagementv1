using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string RoleId { get; set; }  
        public string RoleName { get; set; }

    }
}
