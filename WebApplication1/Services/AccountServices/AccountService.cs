using Newtonsoft.Json;
using PostingManagement.UI.Models.AccountModels;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.AccountServices.Contracts;
using System.Text;

namespace PostingManagement.UI.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public AccountService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<Response<bool>> DeleteUserDetails(string userId, string deletedBy)
        {
            var request = new { UserId = userId, DeleteBy = deletedBy };
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Account/DeleteUser", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<List<GetAllUserVm>>> GetAllUserDetails()
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Account/GetAllUser"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<List<GetAllUserVm>>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<UserViewModel>> GetUserById(string userId)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Account/GetUserById?Id=" + userId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<UserViewModel>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<bool>> SaveUserDetails(CreateUserRequestModel request)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Account/AddNewUser", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<bool>> UpdateUserDetails(UpdateUserRoleRequestModel request)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
               
                using (var response = await httpClient.PutAsync("https://localhost:5000/api/v1/Account/EditUser", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<bool>> IsUserNamePresent(string userName)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Account/IsUserNamePresent?userName=" + userName))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }
    }
}
