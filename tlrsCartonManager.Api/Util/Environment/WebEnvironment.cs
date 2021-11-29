using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Util.Environment
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
                    MachineName = System.Environment.MachineName,
                    UserId = 0,
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
                MachineName = System.Environment.MachineName,
                UserId = string.IsNullOrEmpty(userId)? 0: int.Parse(userId),
                UserName = username
            };
        }
    }
}
