using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Application.Features.Account.Command.DeleteUser
{
    public class DeleteUserCommadHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly IAccountRepository _repository;
        public DeleteUserCommadHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            int id = Convert.ToInt32(EncryptionDecryption.DecryptString(request.UserId));
            bool response =await _repository.DeleteUser(id, request.DeleteBy);
            if (response)
            {
                return new Response<bool>() { Succeeded = true, Data = response,Message="Requested User Deleted successfully " };
            }

            return new Response<bool>() { Succeeded = false, Data = response, Message = "Deletion Failed !!!" };
        }
    }
}
