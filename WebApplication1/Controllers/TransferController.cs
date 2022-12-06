using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models.EmployeeTransferModels;

namespace PostingManagement.UI.Controllers
{
    public class TransferController : Controller
    {
        [HttpGet]
        public IActionResult EmployeeTransferView()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> GetEmployeesDataForTransfer()
        {
         
            List<TransferModel> employeeList = new List<TransferModel>() {
            new TransferModel(){EmployeeId = 1,Name="Vishal",ScaleName="Scale 1 Officer",Scale=1,Designation="Asst Manager",Region="NaviMumbai",Zone="Mumbai",MovementType="InterRegionRequest"},
            new TransferModel(){EmployeeId = 2,Name="Darshan",ScaleName="Scale 2 Officer",Scale=2,Designation="Deputy Branch Head",Region="Andheri East",Zone="MumbaiSuburban",MovementType="InterRegionPromotion"},
            new TransferModel(){EmployeeId = 3,Name="Sumit",ScaleName="Scale 1 Officer",Scale=3,Designation="Deputy Branch Head",Region="Dahisar",Zone="MumbaiSuburban",MovementType="InterRegPromotion"}
            };
            return Json(employeeList);
        }
    }
}
