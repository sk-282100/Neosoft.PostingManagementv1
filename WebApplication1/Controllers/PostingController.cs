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
            ExcelUploadResponseModel responseModel = await _service.UploadExcel(model,uploadedBy);
            ViewBag.ExcelUploadFiletype = HttpContext.Session.GetString("ExcelUploadFiletype");
            ViewBag.ExcelFileTypeCode = HttpContext.Session.GetInt32("ExcelFileTypeCode");
            ViewBag.ExcelUploadResponse = responseModel==null?null: responseModel;
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
        public async Task<IActionResult> GetUploadedDataByBatchId()
        {
            int fileTypeCode = Convert.ToInt32(HttpContext.Session.GetInt32("ExcelFileTypeCode"));
            var result = await _service.GetUploadedRecordsByBatchId(2, fileTypeCode);
            switch (fileTypeCode)
            {
                case 1:
                    var response = JsonConvert.DeserializeObject<List<BranchMaster>>(result);
                    return View(response);

                    //case 2:
                    //    request = new GetExcelDataQuery<DepartmentMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<DepartmentMaster>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 3:
                    //    request = new GetExcelDataQuery<EmployeeMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<EmployeeMaster>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 4:
                    //    request = new GetExcelDataQuery<InterRegionalPromotion>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<InterRegionalPromotion>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 5:
                    //    request = new GetExcelDataQuery<InterRegionRequestTransfer>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<InterRegionRequestTransfer>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 6:
                    //    request = new GetExcelDataQuery<InterZonalPromotion>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<InterZonalPromotion>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 7:
                    //    request = new GetExcelDataQuery<InterZonalRequestTransfer>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //        response = JsonConvert.DeserializeObject<List<InterZonalRequestTransfer>>((string)result);
                    //    else
                    //        response = result;
                    //    return Ok(response);

                    //case 8:
                    //    request = new GetExcelDataQuery<RegionMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //    {
                    //        response = JsonConvert.DeserializeObject<List<RegionMaster>>(Convert.ToString(result));
                    //    }
                    //    else
                    //        response = result;
                    //    return Ok(response);
                    //case 9:
                    //    request = new GetExcelDataQuery<ZoneMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    //    result = await _mediator.Send(request);
                    //    if (result.GetType() == (typeof(object)))
                    //    {
                    //        response = JsonConvert.DeserializeObject<List<ZoneMaster>>(Convert.ToString(result));
                    //    }
                    //    else
                    //        response = result;                    
            }
            return View();
        }

    }
}
