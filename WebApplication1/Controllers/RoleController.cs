using Microsoft.AspNetCore.Mvc;
using PostingManagement.UI.Models;
using PostingManagement.UI.Services.RoleService;

namespace PostingManagement.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        public RoleController(IRoleService roleService, HttpClientHandler clientHandler)
        {
            this.roleService = roleService;
            _clientHandler = clientHandler;
        }

        [Route("Role/GetAllRoles")]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var responseList = await roleService.GetAllRoles();
            return Json(responseList);
        }

        [Route("Role/Addrole")]
        [HttpGet]
        public async Task<IActionResult> Addrole()
        {
            if (TempData.ContainsKey("addRoleResponse"))
            {
                ViewBag.AddRoleResponse = TempData["addRoleResponse"].ToString();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Addrole(RoleModel roleModel)
        {
            var response = await roleService.AddRole(roleModel);
            if (response.Data != null)
            {
                TempData["addRoleResponse"] = response.Data == true ? "true" : "false";
            }
            else
            {
                TempData["addRoleResponse"] = "false";

            }
            return RedirectToAction("Addrole");

        }

        public async Task<IActionResult> RemoveRole(string id)
        {
            var response = await roleService.RemoveRole(id);
            return RedirectToAction("Addrole");
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleService.GetRoleById(id);
            return View(role.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModel roleModel)
        {
            var role = await roleService.EditRole(roleModel);
            return RedirectToAction("Addrole");
        }

        [HttpGet]
        public async Task<IActionResult> IsRoleAlreadyExist(string roleName)
        {
            var response = await roleService.IsRoleAlreadyExist(roleName);
            Console.WriteLine(response.Data);
            return Json(response.Data);
        }
    }
}
