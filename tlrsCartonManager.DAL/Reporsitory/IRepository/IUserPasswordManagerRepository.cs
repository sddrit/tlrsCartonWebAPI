﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Menu;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
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
        Task<IEnumerable<MenuModelsDto>> GetUserMenuRights(string  userName);


        


    }
}
