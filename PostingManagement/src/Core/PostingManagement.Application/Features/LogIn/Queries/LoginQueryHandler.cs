using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.LogIn.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
    {
        private readonly ILoginRepository _repository;
        private readonly IMapper _mapper;
        public LoginQueryHandler(IMapper mapper, ILoginRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            var response = new LoginResponseDto();
            var userDetails = await _repository.GetDetailsByUsername(request.UserName);
            if (userDetails == null)
            {
                response.IsAuthenticated = false;
                response.Message = "User is not  present";
                return response;
            }
            else if (userDetails.Password == request.Password)
            {
                response.IsAuthenticated = true;
                response.Message = "User is authenticated successfully";
                response.UserName = userDetails.UserName;
                response.Role = userDetails.Role;
                return response;
            }

            response.IsAuthenticated = false;
            response.Message = "Something is wrong";
            return response;

        }
    }
}
