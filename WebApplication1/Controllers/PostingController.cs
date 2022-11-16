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
            ViewBag.ExcelUploadFiletype = "Employee Master";
            ViewBag.ExcelFileTypeCode = 3;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> BranchMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "Branch Master";
            ViewBag.ExcelFileTypeCode = 1;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "Department Master";
            ViewBag.ExcelFileTypeCode = 2;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> ZoneMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "Zone Master";
            ViewBag.ExcelFileTypeCode = 9;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> RegionMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "Region Master";
            ViewBag.ExcelFileTypeCode = 8;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            ViewBag.ExcelUploadFiletype = "Inter Zonal Request Transfer";
            ViewBag.ExcelFileTypeCode = 7;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            ViewBag.ExcelUploadFiletype = "Inter Zonal Promotion";
            ViewBag.ExcelFileTypeCode = 6;
            return View("ExcelUploadView");
        }
        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            ViewBag.ExcelUploadFiletype = "Inter Region Request Transfer";
            ViewBag.ExcelFileTypeCode = 5;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            ViewBag.ExcelUploadFiletype = "Inter Regional Promotion";
            ViewBag.ExcelFileTypeCode = 4;
            return View("ExcelUploadView");
        }

        [HttpPost]
        public async Task<IActionResult> ExcelUpload(ExcelUploadViewModel model)
        {
            string uploadedBy = "AdminDarshan";
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model,uploadedBy);
            ViewBag.ExcelUploadResponse = responseModel.Data;
            return View("ExcelUploadView");
        }

        [HttpPost]
        public async Task<IActionResult> ShowUploadHistory(int id)
        {
            var historyList = await _service.GetUploadHistories(id);
            //return PartialView("ShowUploadHistory",historyList.Data);
            return Json(historyList.Data);
        }
    }
}
