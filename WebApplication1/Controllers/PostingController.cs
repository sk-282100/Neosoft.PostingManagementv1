using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostingManagement.UI.CustomActionFilters;
using PostingManagement.UI.Helpers.Constants;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;

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
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.EmployeeMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.EmployeeMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> BranchMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.BranchMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.BranchMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.DepartmentMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.DepartmentMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> ZoneMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.ZoneMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.ZoneMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> RegionMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.RegionMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.RegionMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterZonalRequestTranfer);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterZonalRequestTranfer));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterZonalPromotion);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterZonalPromotion));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }
        [HttpGet]
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterRegionRequestTransfer);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterRegionRequestTransfer));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterRegionPromotion);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterRegionPromotion));
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
                case (int)ExcelFileType.BranchMaster:
                    return RedirectToAction("GetBranchMasterUploadedRecords");

                case (int)ExcelFileType.DepartmentMaster:
                    return RedirectToAction("GetDepartmentMasterUploadedRecords");

                case (int)ExcelFileType.EmployeeMaster:
                    return RedirectToAction("GetEmployeeMasterUploadedRecords");

                case (int)ExcelFileType.InterRegionPromotion:
                    return RedirectToAction("InterRegionalPromotionUploadedRecords");

                case (int)ExcelFileType.InterRegionRequestTransfer:
                    return RedirectToAction("InterRegionRequestTransferUploadedRecords");

                case (int)ExcelFileType.InterZonalPromotion:
                    return RedirectToAction("GetInterZonalPromotionUploadedRecords");

                case (int)ExcelFileType.InterZonalRequestTranfer:
                    return RedirectToAction("GetInterZonalRequestTransferUploadedRecords");

                case (int)ExcelFileType.RegionMaster:
                    return RedirectToAction("GetRegionMasterUploadedRecords");

                case (int)ExcelFileType.ZoneMaster:
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
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<RegionMaster>>(result);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetZoneMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
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
