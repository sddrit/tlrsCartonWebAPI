using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;

namespace tlrsCartonManager.DAL.Reporsitory.IRepositiry
{
    public interface IUserPasswordManagerRepository
    {
        Task<UserPasswordDto> GetSystemUserPasswords(string userID);
        Task<bool> ValidUserName(string userName);
        Task<bool> SaveAllAsync();
        Task<bool> UserLoginTracker(int userid);
        int GetSystemUserID(string userName);
        Task<bool> UserNameAlreadyExist(string userName);
        Task<bool> UpdateSystemUserPasswordAsync(UserDto userdto);


    }
}
