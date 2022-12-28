using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.CustomActionFilters;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;

namespace PostingManagement.UI.Controllers
{
    [LoginFilter]
    public class DashboardController : Controller
    {
        private readonly IExcelUploadService _excelUploadService;
        public DashboardController(IExcelUploadService excelUploadService)
        {
            _excelUploadService = excelUploadService;
        }

        [HttpGet]
        public async Task<ActionResult> ShowDashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUploadHistory(int id)
        {
            var historyList = await _excelUploadService.GetUploadHistories(id);
            return Json(historyList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetWorkFlowStatus()
        {
            var response = await _excelUploadService.GetWorkFlowStaus();
            return Json(response);
        }
    }
}
