using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.Account.Command.DeleteUser
{
    public class DeleteUserCommadHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly IAccountRepository _repository;
        private readonly IDataProtector _protector;
        public DeleteUserCommadHandler(IAccountRepository repository,IDataProtectionProvider provider)
        {
            _repository = repository;
            _protector = provider.CreateProtector("");
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            int id = Convert.ToInt32(_protector.Unprotect(request.UserId));
            bool response =await _repository.DeleteUser(id, request.DeleteBy);
            if (response)
            {
                return new Response<bool>() { Succeeded = true, Data = response,Message="Requested User Deleted successfully " };
            }

            return new Response<bool>() { Succeeded = false, Data = response, Message = "Deletion Failed !!!" };
        }
    }
}
