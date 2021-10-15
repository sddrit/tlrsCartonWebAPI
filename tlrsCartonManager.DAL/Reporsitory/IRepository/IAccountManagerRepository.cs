using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Menu;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IAccountManagerRepository
    {
        Task<UserLoginResponse> Login(UserLoginModel model);
        bool ChangeProfile(UserDto model);
        int GetUserRolePermissionsInt(int userId, string moduleName);
        Task<bool> ChangePasswordAsync(UserPasswordExpiredModel model);
    }
}
