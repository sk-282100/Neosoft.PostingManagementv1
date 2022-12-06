using Newtonsoft.Json;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.TransferService.Contracts;

namespace PostingManagement.UI.Services.TransferService
{
    public class TransferService:ITransferService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public TransferService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<List<EmployeeTransferModel>> GetEmployeesForTransfer()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/TransferList/GetTransferList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<List<EmployeeTransferModel>>>(apiResponse);
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
    }
}
