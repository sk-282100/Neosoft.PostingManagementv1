using Newtonsoft.Json;
using PostingManagement.Domain.Entities;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;
using System.Net.Http;
using System.Text;

namespace PostingManagement.UI.Services.RoleService
{
    public class RoleService : IRoleService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public RoleService(HttpClientHandler clientHandler)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        }
        public async Task<List<RoleModel>> GetAllRoles()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(roleModel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Role/GetAllRoles"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<List<RoleModel>>>(apiResponse);

                    return uploadResult.Data;
                }
            }
        }
        public async Task<Response<bool>> AddRole(RoleModel roleModel)
        {
            using(var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(roleModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Role/AddRole", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                   
                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> RemoveRole(string Id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(Id), Encoding.UTF8, "application/json");
                using (var response = await httpClient.DeleteAsync("https://localhost:5000/api/v1/Role/DeleteRole?id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<RoleModel>> GetRoleById(string roleId)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(roleId), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Role/GetRoleById?id="+ roleId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<RoleModel>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> EditRole(RoleModel roleModel)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(roleModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:5000/api/v1/Role/EditRole", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> IsRoleAlreadyExist(string roleName)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Role/IsRoleAlreadyExist?roleName=" + roleName))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }
    }
}
