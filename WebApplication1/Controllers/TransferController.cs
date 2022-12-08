using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Services.TransferService.Contracts;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PostingManagement.UI.Controllers
{
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
