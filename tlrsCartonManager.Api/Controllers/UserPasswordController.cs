using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using tlrsCartonManager.Api.Error;
using tlrsCartonManager.DAL.Dtos.Menu;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPasswordController : Controller
    {
        private readonly IUserPasswordManagerRepository _userPasswordRepository;
        private readonly ITokenServicesRepository _tokenServiceRepository;

        public UserPasswordController(IUserPasswordManagerRepository userPasswordRepository, ITokenServicesRepository tokenServiceRepository)
        {
            _userPasswordRepository = userPasswordRepository;
            _tokenServiceRepository = tokenServiceRepository;
        }


        [HttpPost("CreateUserPassword")]
        public async Task<ActionResult<UserToken>> RegisterSystemUsers([FromBody] UserDto userPasswordRecords)
        {
            using var hmac = new HMACSHA512();

            if (!ModelState.IsValid)
            {                
                return new JsonErrorResult(new { Message = "User Model Error" }, HttpStatusCode.BadRequest);
            }

            if (await _userPasswordRepository.UserNameAlreadyExist(userPasswordRecords.UserName))
                return new JsonErrorResult(new { Message = "User Name Already Taken" }, HttpStatusCode.Conflict);


            if (await _userPasswordRepository.UpdateSystemUserPasswordAsync(userPasswordRecords))
            {
                return new UserToken
                {
                    UserId = userPasswordRecords.UserName
                };
            }
            else
            {
                return new JsonErrorResult(new { Message = "Something went wrong when saving the record" }, HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpPost("LoginUsers")]
        public async Task<ActionResult<UserToken>> LoginUsers(SystemUserPasswordsDto userPAssword)
        {

            if (!await _userPasswordRepository.ValidUserName(userPAssword.UserID))
            {
                return new JsonErrorResult(new { Message = "Invalid User Name" }, HttpStatusCode.NotFound);
            }

            var systemUserPassword = await _userPasswordRepository.GetSystemUserPasswords(userPAssword.UserID);



            using var hmac = new HMACSHA512(systemUserPassword.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPAssword.PasswordText));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != systemUserPassword.PasswordHash[i])
                {
                    return new JsonErrorResult(new { Message = "Passowrd Not Valid" }, HttpStatusCode.NotFound);
                }
            }

            int systemuserid = _userPasswordRepository.GetSystemUserID(userPAssword.UserID);

            await _userPasswordRepository.UserLoginTracker(systemuserid);

            IEnumerable<MenuModelsDto> lnMenu = new List<MenuModelsDto>();
            lnMenu = _userPasswordRepository.GetUserMenuRights(userPAssword.UserID).Result;
            return new UserToken
            {
                UserId = userPAssword.UserID,
                Token = _tokenServiceRepository.CreateToken(userPAssword.UserID),
                UserRights = lnMenu
            };

        }


        [HttpGet("{userName}/userRights")]
        public Task<IEnumerable<MenuModelsDto>> GetUserRoleMenuList(string userName)
        {
            return _userPasswordRepository.GetUserMenuRights(userName);
        }
    }
}
