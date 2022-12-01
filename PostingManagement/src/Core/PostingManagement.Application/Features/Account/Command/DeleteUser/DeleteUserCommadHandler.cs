using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.DeleteUser
{
    public class DeleteUserCommadHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly IAccountRepository _repository;
        private readonly IDataProtector _dataProtector;
        public DeleteUserCommadHandler(IAccountRepository repository,IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //Decripting the UserId
            int id = Convert.ToInt32(_dataProtector.Unprotect(request.UserId));
            bool response =await _repository.DeleteUser(id, request.DeleteBy);

            //check the deletion status
            if (response)
            {
                return new Response<bool>() { Succeeded = true, Data = response,Message="Requested User Deleted successfully " };
            }

            return new Response<bool>() { Succeeded = false, Data = response, Message = "Deletion Failed !!!" };
        }
    }
}
