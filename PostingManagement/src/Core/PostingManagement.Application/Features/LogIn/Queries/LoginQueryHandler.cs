using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.LogIn.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
    {
        private readonly ILoginRepository _repository;
        public LoginQueryHandler( ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var response = new LoginResponseDto();
            //Getting user details by Username
            var userDetails = await _repository.GetDetailsByUsername(request.UserName);
            //Decryting the Password for comparison 
            var password = EncryptionDecryption.DecryptString(userDetails.Password);
            if (userDetails == null)
            {
                response.IsAuthenticated = false;
                response.Message = "User is not  present";
                return response;
            }
            else if (password == request.Password)
            {
                response.IsAuthenticated = true;
                response.Message = "User is authenticated successfully";
                response.UserName = userDetails.UserName;
                response.Role = await _repository.GetRoleByid(userDetails.RoleId);
                return response;
            }

            response.IsAuthenticated = false;
            response.Message = "Something is wrong";
            return response;
        }
    }
}
