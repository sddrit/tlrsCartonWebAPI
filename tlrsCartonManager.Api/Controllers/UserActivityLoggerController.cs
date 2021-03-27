using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivityLoggerController : Controller
    {
        private readonly IUserActivityLoggerManagerRepository _userActivityLoggerRepository;

        public UserActivityLoggerController(IUserActivityLoggerManagerRepository userActivityLoggerRepository)
        {
            _userActivityLoggerRepository = userActivityLoggerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivityLoggerDto>>> GetUserActivityLoggerList()
        {
            var userList=  await _userActivityLoggerRepository.GetUserActivityLoggerList();
            return Ok(userList);
        }

    }
}
