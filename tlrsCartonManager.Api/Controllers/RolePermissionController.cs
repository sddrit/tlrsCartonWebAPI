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
    public class RolePermissionController : Controller
    {
        private readonly IRolePermissionManagerRepository _menuRoleRepository;
        public RolePermissionController(IRolePermissionManagerRepository menuRoleRepository)
        {
            _menuRoleRepository = menuRoleRepository;
        }        

        [HttpPost]
        public IActionResult AddRolePermission(RoleResponse request)
        {
            if (_menuRoleRepository.AddRolePermission(request))

                return new JsonErrorResult(new { Message = "Role  Created" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Roles  Creation Failed" }, HttpStatusCode.NotFound);            
        }
        [HttpPut]
        public IActionResult UpdateRolePermission(RoleResponse request)
        {
            if (_menuRoleRepository.UpdateRolePermission(request))

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
        [HttpGet("getRolePermission")]
        public async Task<ActionResult<RolePermissionListItem>> GetRolePermissionList()
        {
            var roleList = await _menuRoleRepository.GeRolePermissionList();
            return Ok(roleList);
        }

        [HttpGet("getRolePermission/{id}")]
        public async Task<ActionResult<RolePermissionListItem>> GetRolePermissionListById(int id)
        {
            var roleList = await _menuRoleRepository.GetRolePermissionListById(id);
            return Ok(roleList);
        }
        [HttpGet("getPermissionPendingRole")]
        public async Task<ActionResult<MenuModel>> GetPermissionPendingRoleList()
        {
            var roleList = await _menuRoleRepository.GetPermissionPendingRoleList();
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
