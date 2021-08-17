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
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RoleController : Controller
    {
        private readonly IRolePermissionManagerRepository _menuRoleRepository;

        public RoleController(IRolePermissionManagerRepository menuRoleRepository)

        {
            _menuRoleRepository = menuRoleRepository;
        }        

        [HttpPost]
        [RmsAuthorization("Role Permission", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public IActionResult AddRolePermission(RoleResponse request)
        {
            return Ok(_menuRoleRepository.AddRolePermission(request));                      
        }

        [HttpPut]
        [RmsAuthorization("Role Permission", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public IActionResult UpdateRolePermission(RoleResponse request)
        {
            return Ok(_menuRoleRepository.UpdateRolePermission(request));
        }

        [HttpDelete]
        [RmsAuthorization("Role Permission", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public ActionResult DeleteRole(RoleResponseDelete role)
        {
            return Ok( _menuRoleRepository.DeleteRolePermission(role));
        }


        [HttpGet("getRoles")]
        [RmsAuthorization("Role Permission", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<RoleResponseListItem>> GetRoleList()
        {
            return Ok(await _menuRoleRepository.GetRoleList());          
        }      

        [HttpGet("getRole/{id}")]
        [RmsAuthorization("Role Permission", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<RolePermissionListItem>> GetRolePermissionListById(int id)
        {
            return Ok(await _menuRoleRepository.GetRolePermissionListById(id));          
        }


       

       
       
    }
}
