using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.LoginService.Contracts
{
    public interface ILoginService
    {
        /// <summary>
        /// call the API for Login Authentication 
        /// </summary>
        /// <param name="loginModel">object contains the Username and Password </param>
        /// <returns>LoginResponseModel object contains the Role , Username and Authentication status </returns>
        public Task<LoginResponseModel> Login(LoginModel loginModel);
    }
}
