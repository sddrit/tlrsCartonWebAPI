using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.Menu;
using tlrsCartonManager.DAL.Models;

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
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserRole { get; set; }

        public string Token { get; set; }

        public IEnumerable<UserModulePermission> Permissions { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public bool IsPasswordExpired {get;set;}
        public string TenantName { get; set; }
        public int? Id { get; set; }
        public string CompanyName { get; set; }
    }

    public class UserLoginInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserRole { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public string TenantName { get; set; }

        public string CompanyName { get; set; }

    }
}
