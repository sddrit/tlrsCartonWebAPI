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
            return Ok(_menuRoleRepository.AddRolePermission(request));                      
        }

        [HttpPut]
        public IActionResult UpdateRolePermission(RoleResponse request)
        {
            return Ok(_menuRoleRepository.UpdateRolePermission(request));
        }

        [HttpDelete]
        public ActionResult DeleteRole(RoleResponseDelete role)
        {
            return Ok( _menuRoleRepository.DeleteRolePermission(role));
        }


        [HttpGet("getRole")]
        public async Task<ActionResult<RoleResponseListItem>> GetRoleList()
        {
            return Ok(await _menuRoleRepository.GetRoleList());          
        }      

        [HttpGet("getRolePermission/{id}")]
        public async Task<ActionResult<RolePermissionListItem>> GetRolePermissionListById(int id)
        {
            return Ok(await _menuRoleRepository.GetRolePermissionListById(id));          
        }


       

       
       
    }
}
