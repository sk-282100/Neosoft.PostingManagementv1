using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models;

using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Services.TransferService.Contracts;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PostingManagement.UI.Controllers
{
    //[LoginFilter]
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
        public async Task<IActionResult> GetEmployeesDataForTransfer([FromBody] Pagination pagination)
        {
            DTResponse dtResponse = new DTResponse();
            int numberOfRecords = pagination.data.length;
            int pageNumber = (pagination.data.start / numberOfRecords) + 1;
            var result = await _transferService.GetEmployeesForTransfer(pageNumber, numberOfRecords);
            dtResponse.recordsFiltered = result.TotalRecords;
            dtResponse.recordsTotal = numberOfRecords;
            dtResponse.data = result.Data;
            return Json(dtResponse);
        }        

        [HttpGet]
        public async Task<IActionResult> GetAdditionalEmployeeDetails(int employeeId, string movementType)
        {
            EmployeeDetailsForTransferList employeeDetails = await _transferService.GetEmployeeAddidtionalDetails(employeeId, movementType);
            return Json(employeeDetails);
        }


        public async Task<IActionResult> GetEmployeesDataForTransferByEmployeeId([FromBody] List<int> employeeIdList)
        {
            List<EmployeeTransferModel> selectedEmployeeByCo = await _transferService.GetEmployeesDataForTransferByEmployeeId(employeeIdList);
            return Json(selectedEmployeeByCo);
        }


        public async Task<IActionResult> FinalizeEmployeeTransferViewCo()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FinalizeEmployeeTransferCo([FromBody] List<EmployeeTransferModel> finalizeEmployeeListByCo)
        {
            var result = await _transferService.GenerateEmployeeTransferListCo(finalizeEmployeeListByCo);
            return Json(result);
        }

        [HttpGet]
        public IActionResult MatchingRequestTransferVacancyView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MatchingRequestTransferVacancyData([FromBody] List<int> selectedEmployeeIdLList)
        {
            var result = await _transferService.MatchingEmployeeRequestTransferVacancy(selectedEmployeeIdLList);
            return Json(result);
        }
    }
}
