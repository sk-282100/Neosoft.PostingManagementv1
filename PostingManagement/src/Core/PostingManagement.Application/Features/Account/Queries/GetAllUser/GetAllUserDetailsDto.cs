using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.GetAllUser
{
    public class GetAllUserDetailsDto
    {
        public string UId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
