using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using AutoMapper.QueryableExtensions;
using tlrsCartonManager.DAL.Dtos.Menu;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserPasswordManagerRepository : IUserPasswordManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public UserPasswordManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public int GetSystemUserID(string userName)
        {
            var userList = _tcContext.Users.Where(x => x.UserName == userName).ToList().FirstOrDefault();
            return userList.UserId;
        }

        public async Task<UserPasswordDto> GetSystemUserPasswords(string userID)
        {
            var userName = await _tcContext.Users.SingleOrDefaultAsync(x => x.UserName == userID);

            int systemUserID = userName.UserId;

            var userPasswordLine = await _tcContext.UserPasswords.SingleOrDefaultAsync(x => x.UserId == systemUserID);

            return _mapper.Map<UserPasswordDto>(userPasswordLine);
        }

        public async Task<IEnumerable<MenuModelsDto>> GetUserMenuRights(string userName)
        {
            //Get User Role ID 
            List<MenuModelsDto> MenuModelList = new List<MenuModelsDto>();
            int user = _tcContext.Users.SingleOrDefaultAsync(x => x.UserName == userName).Result.UserRoleId;

            var menuModelUsers = await _tcContext.MenuModelUserRoles.Where(x => x.UserRoleId == user).Include(x=>x.Model).ToListAsync();

            foreach(MenuModelUserRole menuUser in menuModelUsers)
            {
                MenuModelsDto lnMenuModel = new MenuModelsDto();
                lnMenuModel.ModelID = menuUser.Model.ModelCode;
                lnMenuModel.ModelName = menuUser.Model.ModelName;
                List<MenuModelOptionsDto> _lstMenuModelOptions = new List<MenuModelOptionsDto>();

                var userRoleModels = await _tcContext.MenuModelOptionsUserRoles.Where(x => x.UserRoleModelId == menuUser.ModelId).Include(x=>x.FormRight).ToListAsync();

                foreach(MenuModelOptionsUserRole mnuOptUserRole in userRoleModels)
                {
                    MenuModelOptionsDto lnMenuModelUserRole = new MenuModelOptionsDto();
                    lnMenuModelUserRole.ModelID = menuUser.Model.ModelCode;
                    lnMenuModelUserRole.ModelOptionID = mnuOptUserRole.FormRightId;
                    lnMenuModelUserRole.ModelOptionDesc = mnuOptUserRole.FormRight.FormRightName;
                    _lstMenuModelOptions.Add(lnMenuModelUserRole);
                }


                lnMenuModel.ModelOptions = _lstMenuModelOptions;
                MenuModelList.Add(lnMenuModel);
            }


            return MenuModelList;
        }



        public async Task<bool> SaveAllAsync()
        {
            return await _tcContext.SaveChangesAsync() >= 0;
        }

        public async Task<bool> UpdateSystemUserPasswordAsync(UserDto userdto)
        {
            var systemUser = new User();
            systemUser.UserName = userdto.UserName;
            systemUser.UserFullName = userdto.UserFullName;
            systemUser.AppId = userdto.AppId;
            systemUser.EmpId = userdto.EmpId;
            systemUser.UserRoleId = userdto.UserRoleId;
            _tcContext.Users.Add(systemUser);

            await _tcContext.SaveChangesAsync();
            int newUserid = systemUser.UserId;

            using var hmac = new HMACSHA512();
            var userPasswordValue = new UserPassword();
            userPasswordValue.UserId = newUserid;
            userPasswordValue.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userdto.UserPassword));
            userPasswordValue.PasswordSalt = hmac.Key;
            _tcContext.UserPasswords.Add(userPasswordValue);

            return await SaveAllAsync();
        }

        public async Task<bool> UserLoginTracker(int userid)
        {
            var userDetails = await _tcContext.Users.SingleOrDefaultAsync(x => x.UserId == userid);
            userDetails.LastLoginDate = DateTime.Now;

            UserLogger userloger = new UserLogger();
            userloger.UserId = userid;
            userloger.LoginDate = DateTime.Now;
            userloger.ExpiryDate = DateTime.Now.AddDays(1);

            _tcContext.UserLoggers.Add(userloger);

            return await SaveAllAsync();
        }

        public async Task<bool> UserNameAlreadyExist(string userName)
        {
            return await _tcContext.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> ValidUserName(string userName)
        {
            return await _tcContext.Users.AnyAsync(x => x.UserName == userName);
        }


        

    }
}
