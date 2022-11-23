using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdDto
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
