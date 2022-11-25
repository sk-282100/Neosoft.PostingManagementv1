using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostingManagement.UI.Models.AccountModels;
using PostingManagement.UI.Services.AccountServices.Contracts;

namespace PostingManagement.UI.Controllers
{
    public class AccountViewController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountViewController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateUserName()
        {
            List<RoleViewModel> roles = new List<RoleViewModel>()
            {
                new RoleViewModel(){RoleId = 1,RoleName ="CO"},
                new RoleViewModel(){RoleId = 2,RoleName ="ZO"},
                new RoleViewModel(){RoleId = 3,RoleName ="RO"}

            };
            ViewBag.Roles = new SelectList(roles,"RoleId","RoleName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserName(CreateUserRequestModel model)
        {
            model.CreatedBy = "Sumit";
            var response = await _accountService.SaveUserDetails(model);
           
            return RedirectToAction("CreateUserName");
        }
        
        public async Task<IActionResult> DeleteUserName(string id)
        {
            var deletedBy = "Sumit";
            await _accountService.DeleteUserDetails(id, deletedBy);
            return RedirectToAction("CreateUserName");
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoleDetails(string id)
        {
            var model = await _accountService.GetUserById(id);
            List<RoleViewModel> roles = new List<RoleViewModel>()
            {
                new RoleViewModel(){RoleId = 1,RoleName ="CO"},
                new RoleViewModel(){RoleId = 2,RoleName ="ZO"},
                new RoleViewModel(){RoleId = 3,RoleName ="RO"}

            };
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            return View(model.Data);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRoleDetails(UserViewModel model)
        {
            UpdateUserRoleRequestModel request = new();
            request.UId = model.UId;
            request.UserName = model.UserName;
            request.RoleId = model.RoleId;
            request.UpdatedBy = "Sumit";
            await _accountService.UpdateUserDetails(request);
            return RedirectToAction("CreateUserName");
        }
        [HttpGet]
        public async Task<IActionResult> ShowUserRoleDetails()
            {
            var userList = await _accountService.GetAllUserDetails();
            
            return Json(userList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> IsUserNamePresent(string userName)
        {
            var response = await _accountService.IsUserNamePresent(userName);

            return Json(response.Data);
        }

    }
}
