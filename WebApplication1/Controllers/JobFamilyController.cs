using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.CustomActionFilters;
using PostingManagement.UI.Models;
using PostingManagement.UI.Services.JobFamilyServices;
namespace PostingManagement.UI.Controllers
{
    [LoginFilter]
    public class JobFamilyController : Controller
    {
        private readonly IJobFamilyService _jobFamilyService;
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public JobFamilyController(IJobFamilyService jobFamilyService,HttpClientHandler clientHandler)
        {
            _jobFamilyService = jobFamilyService;
            _clientHandler = clientHandler; 
        }

        //[Route("JobFamily/GetAllJobFamily")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobFamily()
        {
            var jobFamilyList = await _jobFamilyService.GetAllJobFamily();
            return Json(jobFamilyList);
        }

        //[Route("JobFamily/AddJobFamily")]
        [HttpGet]
        public async Task<IActionResult> AddJobFamily()
        {
            if (TempData.ContainsKey("addJobFamilyResponse"))
            {
                ViewBag.AddJobFamilyResponse = TempData["addJobFamilyResponse"].ToString();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobFamily(JobFamilyModel jobFamilyModel)
        {
            var response = await _jobFamilyService.AddJobFamily(jobFamilyModel);
            if (response.Data != null)
            {
                TempData["addJobFamilyResponse"] = response.Data == true ? "true" : "false";
            }
            else
            {
                TempData["addJobFamilyResponse"] = "false";

            }
            return RedirectToAction("AddJobFamily");

        }
        public async Task<IActionResult> RemoveJobFamily(string id)
        {
            await _jobFamilyService.RemoveJobFamily(id);
            return RedirectToAction("AddJobFamily");
        }
        [HttpGet]
        public async Task<IActionResult> EditJobFamily(string id)
        {
            var jobFamily = await _jobFamilyService.GetJobFamilyById(id);
            return View(jobFamily.Data);  
        }
        [HttpPost]
        public async Task<IActionResult> EditJobFamily(JobFamilyModel jobFamilyModel)
        {
            var jobFamily = await _jobFamilyService.EditJobFamily(jobFamilyModel);
            return RedirectToAction("AddJobFamily");
        }
        [HttpGet]
        public async Task<IActionResult> IsJobFamilyAlreadyExist(string jobFamilyName)
        {
            var response = await _jobFamilyService.IsJobFamilyAlreadyExist(jobFamilyName);
            return Json(response.Data); 
        }
    }
}
