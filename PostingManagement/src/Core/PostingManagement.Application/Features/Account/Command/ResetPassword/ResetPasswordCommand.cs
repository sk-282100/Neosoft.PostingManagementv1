using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<bool>>
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }

    }
}
