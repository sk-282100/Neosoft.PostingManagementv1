using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Services.TransferService.Contracts;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using PostingManagement.UI.CustomActionFilters;
using PostingManagement.UI.Models;
using Newtonsoft.Json;

namespace PostingManagement.UI.Controllers
{
    [LoginFilter]
    public class TransferController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly ITransferService _transferService;
        public TransferController(ITransferService transferService, IHostingEnvironment environment)
        {
            _transferService = transferService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult EmployeeTransferView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeesDataForTransfer(Pagination pagination)
        {
            DTResponse dtResponse = new DTResponse();
            int numberOfRecords = pagination.data.length;
            int pageNumber = (pagination.data.start / numberOfRecords) + 1;
            List<EmployeeTransferModel> employeeList = await _transferService.GetEmployeesForTransfer(pageNumber,numberOfRecords);
            dtResponse.data = JsonConvert.SerializeObject(employeeList);
            return Json(dtResponse);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetEmployeesDataForTransfer()
        //{
        //    int page = 1;
        //    int pageSize = 5;
        //    List<EmployeeTransferModel> employeeList = await _transferService.GetEmployeesForTransfer(page, pageSize);
        //    return Json(employeeList);            
        //}


        [HttpGet]
        public async Task<IActionResult> GetAdditionalEmployeeDetails(int employeeId, string movementType)
        {
            EmployeeDetailsForTransferList employeeDetails = await _transferService.GetEmployeeAddidtionalDetails(employeeId, movementType);
            return Json(employeeDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesSelectedByCo(int[] employeesId)
        {
            List<EmployeeTransferModel> selectedEmployeeByCo = await _transferService.GetSelectedEmployeesByCo(employeesId);
            return View(selectedEmployeeByCo);
        }
    }
}
