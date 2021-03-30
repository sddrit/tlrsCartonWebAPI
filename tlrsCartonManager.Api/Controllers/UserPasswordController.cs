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

            if (userPasswordRecords == null)
            {
                return BadRequest(ModelState);
            }

            if (await _userPasswordRepository.UserNameAlreadyExist(userPasswordRecords.UserName)) return BadRequest("User Name is taken");


            if (await _userPasswordRepository.UpdateSystemUserPasswordAsync(userPasswordRecords))
            {
                return new UserToken
                {
                    UserId = userPasswordRecords.UserName
                };
            }
            else
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record");
                return StatusCode(500, ModelState);
            }
        }


        [AllowAnonymous]
        [HttpPost("LoginUsers")]
        public async Task<ActionResult<UserToken>> LoginUsers(SystemUserPasswordsDto userPAssword)
        {

            if (!await _userPasswordRepository.ValidUserName(userPAssword.UserID))
            {
                return Unauthorized("Invalid Username");
            }

            var systemUserPassword = await _userPasswordRepository.GetSystemUserPasswords(userPAssword.UserID);



            using var hmac = new HMACSHA512(systemUserPassword.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPAssword.PasswordText));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != systemUserPassword.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            int systemuserid = _userPasswordRepository.GetSystemUserID(userPAssword.UserID);

            await _userPasswordRepository.UserLoginTracker(systemuserid);

            return new UserToken
            {
                UserId = userPAssword.UserID,
                Token = _tokenServiceRepository.CreateToken(userPAssword.UserID)

            };
        }

        [HttpGet("{userName}/userRights")]
        public Task<IEnumerable<MenuRightAttachedUserDto>> GetUserRoleMenuList(string userName)
        {
            return _userPasswordRepository.GetUserMenuRights(userName);
        }


    }
}
