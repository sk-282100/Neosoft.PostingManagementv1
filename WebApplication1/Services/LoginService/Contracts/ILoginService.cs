using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.LoginService.Contracts
{
    public interface ILoginService
    {
        public Task<LoginResponseModel> Login(LoginModel loginModel);
    }
}
