using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models;
using Microsoft.AspNetCore.Http;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using PostingManagement.UI.CustomActionFilters;

namespace PostingManagement.UI.Controllers
{
    [LoginFilter]
    public class PostingController : Controller
    {
        private readonly IExcelUploadService _service;
        private readonly IHttpContextAccessor _httpContext;
        public PostingController(IExcelUploadService service, IHttpContextAccessor httpContext)
        {
            _service = service;
            _httpContext = httpContext;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "EmployeeMaster");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 3);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> BranchMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "BranchMaster");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 1);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "DepartmentMaster");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 2);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");            
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> ZoneMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "ZoneMaster");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 9);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> RegionMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "RegionMaster");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 8);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "InterZonalRequestTransfer");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 7);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "InterZonalPromotion");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 6);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");            
            return View("ExcelUploadView");
        }
        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "InterRegionRequestTransfer");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 5);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "InterRegionalPromotion");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 4);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");            
            return View("ExcelUploadView");
        }

        [HttpPost]
        public async Task<IActionResult> ExcelUpload(ExcelUploadViewModel model)
        {
            string uploadedBy = "AdminDarshan";
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model,uploadedBy);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
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
