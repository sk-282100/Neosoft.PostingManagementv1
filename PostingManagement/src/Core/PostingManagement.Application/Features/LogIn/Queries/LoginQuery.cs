using MediatR;

namespace PostingManagement.Application.Features.LogIn.Queries
{
    public class LoginQuery:IRequest<LoginResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }    
}
