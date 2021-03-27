using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManagerRepository _userRepository;

        public UserController(IUserManagerRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getUsersList")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserList()
        {
            var userList=  await _userRepository.GetUsersList();
            return Ok(userList);
        }

    }
}
