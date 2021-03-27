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
    public class UserLoggerController : Controller
    {
        private readonly IUserLoggerManagerRepository _userLoggerRepository;

        public UserLoggerController(IUserLoggerManagerRepository userLoggerRepository)
        {
            _userLoggerRepository = userLoggerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLoggerDto>>> GetUserLoggerList()
        {
            var userList=  await _userLoggerRepository.GetUserLoggerList();
            return Ok(userList);
        }

    }
}
