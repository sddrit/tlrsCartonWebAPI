using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.DAL.Models.ResponseModels;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.DAL.Models.RoleResponse;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserRoleController : Controller
    {
        private readonly IMenuRoleManagerRepository _menuRoleRepository;
        public UserRoleController(IMenuRoleManagerRepository menuRoleRepository)
        {
            _menuRoleRepository = menuRoleRepository;
        }        

        [HttpPost]
        public IActionResult AddRole(RoleResponse request)
        {
            if (_menuRoleRepository.AddRole(request))

                return new JsonErrorResult(new { Message = "Role  Created" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Roles  Creation Failed" }, HttpStatusCode.NotFound);            
        }
        [HttpGet("getRole")]
        public async Task<ActionResult<RoleResponseListItem>> GetRoleList()
        {
            var roleList = await _menuRoleRepository.GetRoleList();
            return Ok(roleList);
        }
        [HttpGet("getUserRole")]
        public async Task<ActionResult<ViewUserRole>> GetUserRoleList()
        {
            var roleList = await _menuRoleRepository.GetUserRoleList();
            return Ok(roleList);
        }
        [HttpGet("getMenu")]
        public async Task<ActionResult<MenuModel>> GetMenuList()
        {
            var roleList = await _menuRoleRepository.GetMenuList();
            return Ok(roleList);
        }
        [HttpPost("AddRole")]
        public async Task<ActionResult> AddRole(Role role)
        {
            var validateMessage = _menuRoleRepository.ValidateRole(role);
            if (!string.IsNullOrEmpty(validateMessage))
                return new JsonErrorResult(new { Message = validateMessage }, HttpStatusCode.NotFound);

            if (await _menuRoleRepository.AddRole(role))
                return new JsonErrorResult(new { Message = "Role Created" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Role creation failed" }, HttpStatusCode.NotFound);

        }
        [HttpDelete("deleteRole")]
        public async Task<ActionResult> DeleteRole(Role role)
        {      

            if (await _menuRoleRepository.DeleteRole(role))
                return new JsonErrorResult(new { Message = "Role Deleted" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Role deletion failed" }, HttpStatusCode.NotFound);

        }
    }
}
