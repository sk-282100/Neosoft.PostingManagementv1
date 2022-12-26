using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models.TriggerModels;
using PostingManagement.UI.Services.TriggerServices.Contracts;

namespace PostingManagement.UI.Controllers
{
    public class TriggerViewController : Controller
    {
        private readonly ITriggerService _triggerService;
        public TriggerViewController(ITriggerService triggerService)
        {
            _triggerService = triggerService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowTriggerTable()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrigger()
        {
            var response = await _triggerService.GetAllTrigger();
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScale()
        {
            var response = await _triggerService.GetAllScale();
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTriggerById(string id)
        {
            var response = await _triggerService.GetTriggerById(id);
            return Json(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTrigger([FromBody]CreateTriggerRequestModel model )
        {
            var response = await _triggerService.SaveTrigger(model);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrigger(string id)
        {
            var response = await _triggerService.DeleteTrigger(id);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrigger([FromBody] TriggerViewModel model)
        {
            var response = await _triggerService.UpdateTrigger(model);
            return Json(response);
        }
    }
}
