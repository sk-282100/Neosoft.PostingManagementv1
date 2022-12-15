using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Login;
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
        /// <summary>
        /// call the Api for Send OTP to User email by UserName
        /// </summary>
        /// <param name="request">object contains username (string) for sending the OTP </param>
        /// <returns>Response of SendOTPResponseModel type model contains OTP Expirytime and Email Status</returns>
        public Task<Response<SendOTPResponseModel>> SendOTP(SendOTPRequestModel request);
        /// <summary> 
        /// call the Api for Verifying the  OTP 
        /// </summary>
        /// <param name="userName">username (string) for verifying the OTP </param>
        /// <param name="enteredOTP">entered OTP</param>
        /// <returns>Response of bool represents the Verification Status</returns>
        public Task<Response<bool>> VerifyOTP(VerifyOTPRequestModel request);

    }
}
