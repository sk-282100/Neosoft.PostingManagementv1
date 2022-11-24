﻿using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Responses;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.RoleService;
using System.Reflection;

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
        public async Task <IActionResult> GetAllRoles()
        {
            var responseList = await roleService.GetAllRoles();
            return Json(responseList);
        }
        [Route("Role/Addrole")]
        [HttpGet]
        public async Task<IActionResult> Addrole()
        
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Addrole(RoleModel roleModel )
        {
            var response = await roleService.AddRole(roleModel);
            ViewBag.AddRoleResponse = response == null ? null : response;
            
            return View();
           
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
    }
}