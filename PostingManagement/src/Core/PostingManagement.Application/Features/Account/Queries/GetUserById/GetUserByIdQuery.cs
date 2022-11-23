using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdQuery :IRequest<Response<GetUserByIdDto>>
    {
        public int UId { get; set; } 
    }
}
