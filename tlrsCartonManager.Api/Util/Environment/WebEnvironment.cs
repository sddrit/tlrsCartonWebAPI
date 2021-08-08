using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Util.Enviroment
{
    public class WebEnvironment : IEnvironment
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserManagerRepository _userManagerRepository;
        public WebEnvironment(IHttpContextAccessor httpContextAccessor, IUserManagerRepository userManagerRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManagerRepository = userManagerRepository;
        }
        public CurrentEnvironment GetCurrentEnvironment()
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return new CurrentEnvironment()
                {
                    Service = "Web API",
                    MachineName = Environment.MachineName,
                    UserId = null,
                    UserName = "admin"
                };
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var username = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = _userManagerRepository.GetUserById(int.Parse(userId)).Result;
                username = user.UserName;
            }

            return new CurrentEnvironment()
            {
                Service = "Web API",
                MachineName = Environment.MachineName,
                UserId = string.IsNullOrEmpty(userId)? null: long.Parse(userId),
                UserName = username
            };
        }
    }
}
