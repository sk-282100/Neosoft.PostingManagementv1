using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.TransferService.Contracts;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace PostingManagement.UI.Services.TransferService
{
    public class TransferService:ITransferService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public TransferService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<TransferListReponseMVC> GetEmployeesForTransfer(int pageNumber, int numberOfRecords)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/TransferList/GetTransferList?pageNumber=" + pageNumber + "&numberOfRecords=" + numberOfRecords))
                //using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/TransferList/GetTransferList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<TransferListReponseMVC>>(apiResponse);
                    return uploadResult.Data;
                }
            }
        }

        public async Task<EmployeeDetailsForTransferList> GetEmployeeAddidtionalDetails(int employeeId,string movementType)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/TransferList/GetAdditionalEmployeeDetails?employeeId=" + employeeId + "&movementType="+movementType))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<EmployeeDetailsForTransferList>>(apiResponse);
                    return uploadResult.Data;
                }
            }
        }


        public async Task<List<EmployeeTransferModel>> GetEmployeesDataForTransferByEmployeeId(List<int> emoloyeeidList)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                var content = new StringContent(JsonConvert.SerializeObject(emoloyeeidList), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/TransferList/GetTransferListByEmployeeId", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<List<EmployeeTransferModel>>>(apiResponse);
                    return uploadResult.Data;
                }
            }

        }

        public async Task<ZOTransferListReponse> GenerateEmployeeTransferListCo (List<EmployeeTransferModel> EmployeesSelectedByCo)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                var content = new StringContent(JsonConvert.SerializeObject(EmployeesSelectedByCo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/TransferList/TransferListForZO", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<ZOTransferListReponse>>(apiResponse);
                    return uploadResult.Data;
                }
            }
        }
    }
}
