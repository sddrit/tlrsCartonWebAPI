using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.Menu;

namespace tlrsCartonManager.DAL.Dtos
{
    public class UserToken
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public IEnumerable<MenuModelsDto> UserRights { get; set; }
    }

    public class UserLoginResponse
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public IEnumerable<UserModulePermission> Permissions { get; set; }
    }
}
