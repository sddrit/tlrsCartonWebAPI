using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<RoleResponseListItem>> GetRoleList()
        {
            return Ok(await _menuRoleRepository.GetRoleList());          
        }      

        [HttpGet("getRole/{id}")]
        public async Task<ActionResult<RolePermissionListItem>> GetRolePermissionListById(int id)
        {
            return Ok(await _menuRoleRepository.GetRolePermissionListById(id));          
        }

    }
}
