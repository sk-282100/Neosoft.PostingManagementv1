using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.IsUserNamePresent
{
    public class IsUserNamePresentQuery :IRequest<Response<bool>>
    {
        public string UserName { get; set; }
    }
}
