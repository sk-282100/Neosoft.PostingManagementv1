using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;

namespace PostingManagement.UI.Controllers
{
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
            //historyList.Data
            var data = from s in historyList.Data
                       group s by new { date = new DateTime(s.Date.Year, s.Date.Month, 1) } into g
                       select new
                       {
                           UploadDate = g.Key.date,
                           TotalNumofRows = g.Sum(x => x.NumberOfRows),
                           insertedRow = g.Sum(x => x.InsertedRows)
                       };
            return Json(historyList.Data);
        }
    }
}
