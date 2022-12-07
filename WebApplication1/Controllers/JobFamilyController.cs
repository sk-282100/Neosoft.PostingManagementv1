using Microsoft.AspNetCore.Mvc;

namespace PostingManagement.UI.Controllers
{
    public class JobFamilyController : Controller
    {
        [Route("JobFamily/GetAllJobFamily")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobFamily()
        {
            return View();
        }
    }
}
