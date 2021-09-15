using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.Services.User;
using tlrsCartonManager.Services.Report;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManagerRepository _userRepository;
        private readonly UserService _userService;

        public UserController(IUserManagerRepository userRepository, UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserbyId(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpGet("getUser")]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<UserSerachDto>> SearchUser(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            return Ok(await _userService.SearchUser(columnValue, searchColumn, sortOrder, pageIndex, pageSize));
        }

        [HttpPost]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<ActionResult<User>> AddUser(UserDto request)
        {
            return Ok(await _userService.CreateUser(request));
        }

        [HttpPut]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public async Task<ActionResult<User>> UpdateUser(UserDto request)
        {
            return Ok(await _userService.UpdateUser(request));
        }

        [HttpPost("Reset")]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public async Task<ActionResult<User>> ResetUser(UserDto request)
        {
            return Ok(await _userService.ResetUser(request));
        }

       

        [HttpDelete]
        [RmsAuthorization("User", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public async Task<ActionResult<User>> DeleteUser(UserDto request)
        {
            return Ok(await _userService.DeleteUser(request));
        }
    }
}
