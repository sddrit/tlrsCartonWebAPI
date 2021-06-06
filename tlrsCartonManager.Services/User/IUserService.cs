using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.Services.User
{
   public  interface IUserService
    {
        Task<DAL.Dtos.UserToken> Login(DAL.Dtos.SystemUserPasswordsDto userPassword);
        Task<DAL.Models.User> CreateUser(DAL.Dtos.UserDto user, int createdUserId);
       
    }
}
