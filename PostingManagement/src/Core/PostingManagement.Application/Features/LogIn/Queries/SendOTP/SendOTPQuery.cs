using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.LogIn.Queries.SendOTP
{
    public class SendOTPQuery :IRequest<Response<SendOTPDto>>
    {
        public string Username { get; set; }
    }
}
