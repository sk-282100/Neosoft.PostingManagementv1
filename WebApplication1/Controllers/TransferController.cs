using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Services.TransferService.Contracts;

namespace PostingManagement.UI.Controllers
{
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;
        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public IActionResult EmployeeTransferView()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesDataForTransfer()
        {
            List<EmployeeTransferModel> employeeList = await _transferService.GetEmployeesForTransfer();

            return Json(employeeList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalEmployeeDetails(int employeeId, string movementType)
        {
            EmployeeDetailsForTransferList employeeDetails = await _transferService.GetEmployeeAddidtionalDetails(employeeId, movementType);
            return Json(employeeDetails);
        }
    }
}
