using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.RoleResponse
{
    //public class Role
    //{
    //    public string Name { get; set; }
    //    public ICollection<MenuModelUserRole> MenuUserRoleList { get; set; }
    //}
    public class RoleResponse
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
        public ICollection<RolePermissionListItem> RolePermissionList { get; set; }
       

    }
    public class MenuRole
    {
        public int MenuId { get; set; }
        public ICollection<RolePermissionListItem> MenuActionRoleList { get; set; }
    }
    public class MenuActionRole
    {
        public int ActionId { get; set; }
    }
    public class MenuActionRoleUtd
    {
        public int MenuId { get; set; }
        public int ActionId { get; set; }
    }
    public class RoleResponseListItem
    {        
        public int Id { get; set; }
        public string Description { get; set; }     

        public int UserId { get; set; }

    }
    public class RolePermissionListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool View { get; set; }

    }
}
