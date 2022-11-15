using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;

namespace PostingManagement.UI.Controllers
{

    public class PostingController : Controller
    {
        private readonly IExcelUploadService _service;
        public PostingController(IExcelUploadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "EmployeeMaster";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExcelUpload(ExcelUploadViewModel model)
        {
            string uploadedBy = "AdminDarshan";
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model,uploadedBy);
            ViewBag.ExcelUploadResponse = responseModel.Data;
            return View();
        }
        [HttpGet]
        public IActionResult ShowEmployeeMaster()
        {
            return View();
        }
    }
}
