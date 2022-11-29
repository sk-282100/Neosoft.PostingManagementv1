using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models;
using Microsoft.AspNetCore.Http;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using PostingManagement.UI.CustomActionFilters;
using Newtonsoft.Json;

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
            HttpContext.Session.SetString("ExcelUploadFiletype", "Employee Master");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 3);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> BranchMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Branch Master");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 1);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Department Master");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 2);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> ZoneMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Zone Master");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 9);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> RegionMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Region Master");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 8);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Inter-Zonal Request Transfer");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 7);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Inter-Zonal Promotion");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 6);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }
        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Inter-Region Request Transfer");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 5);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", "Inter-Regional Promotion");
            HttpContext.Session.SetInt32("ExcelFileTypeCode", 4);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpPost]
        public async Task<IActionResult> ExcelUpload(ExcelUploadViewModel model)
        {
            string uploadedBy = "AdminDarshan";
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model, uploadedBy);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            ViewBag.ExcelUploadResponse = responseModel == null ? null : responseModel;
            return View("ExcelUploadView");
        }

        [HttpPost]
        public async Task<IActionResult> ShowUploadHistory(int id)
        {
            var historyList = await _service.GetUploadHistories(id);
            //return PartialView("ShowUploadHistory",historyList.Data);            
            return Json(historyList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetUploadedDataByBatchId(int id)
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            HttpContext.Session.SetInt32("batchId", id);

            switch (fileTypeCode)
            {
                case 1:
                    return RedirectToAction("GetBranchMasterUploadedRecords");

                case 2:
                    return RedirectToAction("GetDepartmentMasterUploadedRecords");

                case 3:
                    return RedirectToAction("GetEmployeeMasterUploadedRecords");

                case 4:
                    return RedirectToAction("InterRegionalPromotionUploadedRecords");

                case 5:
                    return RedirectToAction("InterRegionRequestTransferUploadedRecords");

                case 6:
                    return RedirectToAction("GetInterZonalPromotionUploadedRecords");

                case 7:
                    return RedirectToAction("GetInterZonalRequestTransferUploadedRecords");

                case 8:
                    return RedirectToAction("GetRegionMasterUploadedRecords");

                case 9:
                    return RedirectToAction("GetZoneMasterUploadedRecords");

            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<EmployeeMaster>>(result);            
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetInterZonalPromotionUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterZonalPromotion>>(result);
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetInterZonalRequestTransferUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterZonalRequestTransfer>>(result);
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetRegionMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(1, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<RegionMaster>>(result);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetZoneMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(2, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<ZoneMaster>>(result);
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetBranchMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<BranchMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<DepartmentMaster>>(result);
            return Json(response);
        }        

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterRegionalPromotion>>(result);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterRegionRequestTransfer>>(result);
            return Json(response);
        }
    }
}
