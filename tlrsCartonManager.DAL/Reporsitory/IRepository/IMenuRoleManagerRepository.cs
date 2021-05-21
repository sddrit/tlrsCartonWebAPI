using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.RoleResponse;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IMenuRoleManagerRepository
    {
        bool AddRole(RoleResponse response);
        Task<IEnumerable<RoleResponseListItem>> GetRoleList();
        Task<IEnumerable<MenuListItem>> GetMenuList();


    }
}
