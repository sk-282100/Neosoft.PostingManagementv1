using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class UserDetailsVm
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
