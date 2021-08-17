using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using tlrsCartonManager.Api.Util.Permission;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Util.Authorization
{
    public class AuthorizeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountManagerRepository _accountManagerRepository;

        public AuthorizeService(IHttpContextAccessor httpContextAccessor, IAccountManagerRepository accountManagerRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountManagerRepository = accountManagerRepository;
        }

        public bool HasPermission(string moduleName, ModulePermission permission)
        {

            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var permissionInt = _accountManagerRepository.GetUserRolePermissionsInt(userId, moduleName.ToLower());

            int[] permissionClaim = permissionInt.GetPermissions();

            if (permissionClaim.Contains((int)permission))
            {               
                return true;
            }

            return false;
        }
    }
}
