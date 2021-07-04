using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.DAL.Models.ResponseModels;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DashBoardController : Controller
    {
        private readonly IDashBoardManagerRepository _dashBoardRepository;

        public DashBoardController(IDashBoardManagerRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }

        [HttpGet("getweeklyStatusbyRequetType")]
        public async Task<ActionResult> GetWeeklyWoStatus()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWoStatusAsync());
            
        }

        [HttpGet("getweeklyStatusbyWoType")]
        public async Task<ActionResult> GetWeeklyWoStatusByType()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWoStatusByTypeAsync());

        }
    }
}
