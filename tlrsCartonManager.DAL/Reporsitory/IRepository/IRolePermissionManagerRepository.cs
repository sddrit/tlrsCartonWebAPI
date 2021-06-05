﻿using System;
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
    public interface IRolePermissionManagerRepository
    {
        bool AddRolePermission(RoleResponse response);
        bool UpdateRolePermission(RoleResponse role);
        bool SaveRolePermission(RoleResponse response, string transactionType);

        Task<IEnumerable<RoleResponseListItem>> GetRoleList();
        Task<IEnumerable<ViewUserRole>> GetPermissionPendingRoleList();
        Task<IEnumerable<RolePermissionListItem>> GeRolePermissionList();
        Task<bool> AddRole(Role role);
        Task<bool> DeleteRole(Role role);
        string ValidateRole(Role role);
        Task<IEnumerable<RolePermissionListItem>> GetRolePermissionListById(int id);
    }
}