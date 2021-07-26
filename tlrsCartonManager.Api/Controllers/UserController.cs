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

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserbyId(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<UserSerachDto>> SearchUser(string columnValue, int pageIndex, int pageSize)
        {
            return Ok(await _userService.SearchUser(columnValue, pageIndex, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDto request)
        {
            return Ok(await _userService.CreateUser(request));
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(UserDto request)
        {
            return Ok(await _userService.UpdateUser(request));
        }

        [HttpPost("Reset")]
        public async Task<ActionResult<User>> ResetUser(UserDto request)
        {
            return Ok(await _userService.ResetUser(request));
        }

       

        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser(UserDto request)
        {
            return Ok(await _userService.DeleteUser(request));
        }
    }
}
