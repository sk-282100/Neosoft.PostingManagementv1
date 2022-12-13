using Newtonsoft.Json;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Login;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.LoginService.Contracts;
using System.Text;

namespace PostingManagement.UI.Services.LoginService
{
    public class LoginService : ILoginService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public LoginService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<LoginResponseModel> Login(LoginModel loginModel)
        {            
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse);
                    return uploadResult;
                }
            }
        }
        public async Task<Response<SendOTPResponseModel>> SendOTP(SendOTPRequestModel request)
        {
            //HTTP Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Login/SendOTP", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<SendOTPResponseModel>>(apiResponse);
                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> VerifyOTP(VerifyOTPRequestModel request)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Login/VerifyOTP", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return uploadResult;
                }
            }
        }
    }
}
