using Newtonsoft.Json;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Models.TriggerModels;
using PostingManagement.UI.Services.TriggerServices.Contracts;
using System.Text;

namespace PostingManagement.UI.Services.TriggerServices
{
    public class TriggerService : ITriggerService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public TriggerService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<Response<bool>> DeleteTrigger(string triggerId)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Trigger/DeleteTrigger?triggerId="+ triggerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<List<GetAllTriggerVm>>> GetAllTrigger()
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Trigger/GetAllTriggers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<List<GetAllTriggerVm>>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<TriggerViewModel>> GetTriggerById(string triggerId)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Trigger/GetTriggerById?Id="+triggerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<TriggerViewModel>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<bool>> SaveTrigger(CreateTriggerRequestModel request)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/Trigger/AddNewTrigger",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<bool>> UpdateTrigger(TriggerViewModel request)
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:5000/api/v1/Trigger/UpdateTrigger",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Response<List<ScaleViewModel>>> GetAllScale()
        {
            //Client Handler Part
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/Scale/GetAllScale"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<List<ScaleViewModel>>>(apiResponse);
                    return result;
                }
            }
        }
    }
}
