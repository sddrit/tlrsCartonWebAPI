using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Api.Util.Permission;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Util.Authorization
{
    public class RmsAuthorization : TypeFilterAttribute
    {
        public RmsAuthorization(string moduleName, tlrsCartonManager.Core.Enums.ModulePermission permission)
            : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { moduleName, permission };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly string _moduleName;
        private readonly ModulePermission _permission;
        private readonly IAccountManagerRepository _accountManagerRepository;

        public AuthorizeActionFilter(string moduleName, ModulePermission permission, IAccountManagerRepository accountManagerRepository)
        {
            _moduleName = moduleName;
            _permission = permission;
            _accountManagerRepository = accountManagerRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            int userId = Convert.ToInt32(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var permission = _accountManagerRepository.GetUserRolePermissionsInt(userId, _moduleName.ToLower());

            int[] permissionClaim = permission.GetPermissions();          

            if (permissionClaim == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ContentType = "application/json",
                    Content = System.Text.Json.JsonSerializer.Serialize(new { error = "Unauthorized" })
                };

                return;
            }

            if (!permissionClaim.Select(item => (ModulePermission)item)
                .Any(item => item.HasFlag(_permission)))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ContentType = "application/json",
                    Content = System.Text.Json.JsonSerializer.Serialize(new { error = "Unauthorized" })
                };
                return;
            }
        }


    }
}
