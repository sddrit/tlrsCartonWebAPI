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

        //[HttpGet("getUsersList")]
        //public async Task<ActionResult<IEnumerable<UserDto>>> GetUserList()
        //{
        //    var userList=  await _userRepository.GetUsersList();
        //    return Ok(userList);
        //}
        [HttpGet("getUser")]
        public async Task<ActionResult<UserSerachDto>> SearchUser(string columnValue, int pageIndex, int pageSize)
        {
            var userList = await _userRepository.SearchUser(columnValue, pageIndex, pageSize);
            return Ok(userList);
        }
        [HttpPost("addUser")]
        public async Task<ActionResult<User>> AddUser(UserDto request)
        {
            return   Ok (await _userService.CreateUser(request));
           
        }
    }
}
