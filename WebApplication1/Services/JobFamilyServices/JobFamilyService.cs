using Newtonsoft.Json;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using PostingManagement.UI.Models;
using System.Net.Http;
using System.Text;

namespace PostingManagement.UI.Services.JobFamilyServices
{
    public class JobFamilyService :IJobFamilyService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public JobFamilyService(HttpClientHandler clientHandler)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<List<JobFamilyModel>> GetAllJobFamily()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/JobFamily/GetAllJobFamily"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var getAllResult = JsonConvert.DeserializeObject<Response<List<JobFamilyModel>>>(apiResponse);

                    return getAllResult.Data;
                }
            }
        }
        public async Task<Response<bool>> AddJobFamily(JobFamilyModel jobFamilyModel)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobFamilyModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/JobFamily/AddJobFamily", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var addJobFamilyResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return addJobFamilyResult;
                }
            }
        }
        public async Task<Response<bool>> RemoveJobFamily(string Id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:5000/api/v1/JobFamily/DeleteJobFamily?id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<JobFamilyModel>> GetJobFamilyById(string id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(roleId), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/JobFamily/GetJobFamilyById?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<JobFamilyModel>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> EditJobFamily(JobFamilyModel jobFamilyModel)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobFamilyModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:5000/api/v1/JobFamily/EditJobFamily", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }
        public async Task<Response<bool>> IsJobFamilyAlreadyExist(string jobFamilyName)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/JobFamily/IsJobFamilyAlreadyExist?jobFamilyName=" + jobFamilyName))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);

                    return uploadResult;
                }
            }
        }


    }
}
