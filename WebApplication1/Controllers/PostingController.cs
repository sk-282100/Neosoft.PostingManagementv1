using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostingManagement.UI.CustomActionFilters;
using PostingManagement.UI.Helpers.Constants;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Newtonsoft.Json.Linq;

namespace PostingManagement.UI.Controllers
{
    [LoginFilter]
    public class PostingController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IExcelUploadService _service;
        private readonly IHttpContextAccessor _httpContext;
        public PostingController(IExcelUploadService service, IHttpContextAccessor httpContext, IHostingEnvironment environment)
        {
            _environment = environment;
            _service = service;
            _httpContext = httpContext;
        }

        [HttpGet]
        //Employee Excel Upload Using FileType Code
        public async Task<IActionResult> EmployeeMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.EmployeeMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.EmployeeMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //Branch Excel Upload Using FileType Code
        public async Task<IActionResult> BranchMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.BranchMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.BranchMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //Department Excel Upload Using FileType Code
        public async Task<IActionResult> DepartmentMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.DepartmentMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.DepartmentMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //zone Excel Upload Using FileType Code
        public async Task<IActionResult> ZoneMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.ZoneMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.ZoneMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //Region Excel Upload Using FileType Code
        public async Task<IActionResult> RegionMasterUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.RegionMaster);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.RegionMaster));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //Department Excel Upload Using FileType Code
        public async Task<IActionResult> InterZonalRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterZonalRequestTranfer);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterZonalRequestTranfer));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //InterZone Excel Upload Using FileType Code Of Respective Zone
        public async Task<IActionResult> InterZonalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterZonalPromotion);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterZonalPromotion));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //InterRegionTransfer Excel Upload Using FileType Code Of Respective Region
        public async Task<IActionResult> InterRegionRequestTransferUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterRegionRequestTransfer);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterRegionRequestTransfer));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpGet]
        //InterRegionPromotion Excel Upload Using FileType Code Of Respective Region
        public async Task<IActionResult> InterRegionalPromotionUpload()
        {
            HttpContext.Session.SetString("ExcelUploadFiletype", ExcelFileUploadName.InterRegionPromotion);
            HttpContext.Session.SetInt32("ExcelFileTypeCode", Convert.ToInt32(ExcelFileType.InterRegionPromotion));
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            return View("ExcelUploadView");
        }

        [HttpPost]
        //Upload Excel
        public async Task<IActionResult> ExcelUpload(ExcelUploadViewModel model)
        {
            string wwPath = this._environment.WebRootPath;
            string contentPath = this._environment.ContentRootPath;
            string uploadedBy = "AdminDarshan";
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model, uploadedBy);
            var excelType = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelUploadFiletype = excelType;
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            ViewBag.ExcelUploadResponse = responseModel == null ? null : responseModel;
            if(responseModel.Data.UploadStatus.ToLower() == "success")
            {
                string path = Path.Combine(this._environment.WebRootPath, "UploadedFiles", excelType, DateTime.Now.ToShortDateString().Replace("/", ""));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // List<string> uploadedFiles = new List<string>();
                IFormFile postedFiles = model.ExcelFile;
                string fileName = Path.GetFileName(postedFiles.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFiles.CopyTo(stream);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }
           


            return View("ExcelUploadView");
        }

        [HttpPost]
        //Show The Upload History By Using Respective Upload Id 
        public async Task<IActionResult> ShowUploadHistory(int id)
        {
            var historyList = await _service.GetUploadHistories(id);
            return Json(historyList.Data);
        }

        [HttpGet]
        //Show All History Uploaded Data By Using Batch Id
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
        //Get All Employee History Data of Uploaded Record By Using BatchId 
        public async Task<IActionResult> GetEmployeeMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<EmployeeMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All Employee History Data of Uploaded Record By Using BatchId 
        public async Task<IActionResult> GetInterZonalPromotionUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterZonalPromotion>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All InterZoneTransfer History Data od Uploaded Record By Using BatchId
        public async Task<IActionResult> GetInterZonalRequestTransferUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterZonalRequestTransfer>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All Region History Data of Uploaded Record By Using BatchId
        public async Task<IActionResult> GetRegionMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<RegionMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All Zone History Data of Uploaded Record By Using BatchId
        public async Task<IActionResult> GetZoneMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<ZoneMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All Branch History Data of Uploaded Record By Using BatchId
        public async Task<IActionResult> GetBranchMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<BranchMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All Department History Data of Uploaded Record By Using BatchId
        public async Task<IActionResult> GetDepartmentMasterUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<DepartmentMaster>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All InterRegionPromotion History Data of Uploaded Record By Using BatchId
        public async Task<IActionResult> InterRegionalPromotionUploadedRecords()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            int batchId = Convert.ToInt32(HttpContext.Session.GetInt32("batchId"));
            var result = await _service.GetUploadedRecordsByBatchId(batchId, fileTypeCode);
            var response = JsonConvert.DeserializeObject<List<InterRegionalPromotion>>(result);
            return Json(response);
        }

        [HttpGet]
        //Get All InterRegionTransfer History Data of Uploaded Record By Using BatchId
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
