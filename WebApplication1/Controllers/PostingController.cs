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
            ViewBag.ExcelFileTypeCode = 3;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> BranchMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "BranchMaster";
            ViewBag.ExcelFileTypeCode = 1;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentMasterUpload()
        {
           
            ViewBag.ExcelUploadFiletype = "DepartmentMaster";
            ViewBag.ExcelFileTypeCode = 2;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> ZoneMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "ZoneMaster";
            ViewBag.ExcelFileTypeCode = 9;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> RegionMasterUpload()
        {
            ViewBag.ExcelUploadFiletype = "RegionMaster";
            ViewBag.ExcelFileTypeCode = 8;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            ViewBag.ExcelUploadFiletype = "InterZonalRequestTransfer";
            ViewBag.ExcelFileTypeCode = 7;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            ViewBag.ExcelUploadFiletype = "InterZonalPromotion";
            ViewBag.ExcelFileTypeCode = 6;
            return View("ExcelUploadView");
        }
        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            ViewBag.ExcelUploadFiletype = "InterRegionRequestTransfer";
            ViewBag.ExcelFileTypeCode = 5;
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            ViewBag.ExcelUploadFiletype = "InterRegionalPromotion";
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
            return PartialView("ShowUploadHistory",historyList.Data);
        }
    }
}
