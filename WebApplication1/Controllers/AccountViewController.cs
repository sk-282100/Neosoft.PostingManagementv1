using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.AccountModels;
using PostingManagement.UI.Services.AccountServices.Contracts;
using PostingManagement.UI.Services.RoleService;

namespace PostingManagement.UI.Controllers
{
    public class AccountViewController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        public AccountViewController(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateUserName()
        {
            List<RoleModel> roles = await _roleService.GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            if (TempData.ContainsKey("addUserResponse"))
            {
                ViewBag.CreateUserResponse = TempData["addUserResponse"].ToString();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserName(CreateUserRequestModel model)
        {
            model.CreatedBy = HttpContext.Session.GetString("Username");
            var response = await _accountService.SaveUserDetails(model);
            if (response.Data != null)
            {
                TempData["addUserResponse"] = response.Data == true ? "true" : "false";
            }
            else
            {
                TempData["addUserResponse"] = "false";

            }
            return RedirectToAction("CreateUserName");
        }

        public async Task<IActionResult> DeleteUserName(string id)
        {
            var deletedBy = HttpContext.Session.GetString("Username");
            await _accountService.DeleteUserDetails(id, deletedBy);
            return RedirectToAction("CreateUserName");
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoleDetails(string id, string currentRole)
        {
            var model = await _accountService.GetUserById(id);

            List<RoleModel> roles = await _roleService.GetAllRoles();

            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            ViewBag.UserCurrentRole = currentRole;
            return View(model.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoleDetails(UserViewModel model)
        {
            UpdateUserRoleRequestModel request = new();
            request.UId = model.UId;
            request.UserName = model.UserName;
            request.RoleId = model.RoleId;
            request.UpdatedBy = HttpContext.Session.GetString("Username");
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
