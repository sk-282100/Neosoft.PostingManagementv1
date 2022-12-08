using Newtonsoft.Json;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.TransferService.Contracts;
using System.Data;
using System.Reflection;

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
        public DataTable ListToDataTable(List<EmployeeTransferModel> items)
        {
            DataTable dataTable = new DataTable(typeof(EmployeeTransferModel).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(EmployeeTransferModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (EmployeeTransferModel item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
