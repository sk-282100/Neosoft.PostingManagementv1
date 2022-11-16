using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.LogIn.Queries
{
    public class LoginResponseDto
    {        
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
