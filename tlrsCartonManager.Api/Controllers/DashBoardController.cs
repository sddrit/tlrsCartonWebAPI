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

        [HttpGet("getWeeklyStatusbyRequetType")]
        public async Task<ActionResult> GetWeeklyWoStatus()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWoStatusAsync());
            
        }

        [HttpGet("getWeeklyPendingRetrievalByWoType")]
        public async Task<ActionResult> GetWeeklyWoStatusByType()
        {
            return Ok(await _dashBoardRepository.GetWeelklyPendingRetrievalByTypeAsync());

        }

        [HttpGet("getDailyDashBoard")]
        public async Task<ActionResult> GetDashBoard()
        {
            return Ok(await _dashBoardRepository.GetDailyDashBoard());

        }

        [HttpGet("getWeeklyCartonInAndConfirm")]
        public async Task<ActionResult> GetWeeklyCartonInAndConfirm()
        {
            return Ok(await _dashBoardRepository.GetWeelklyCartonsInAndConfirm());

        }

        [HttpGet("getWeeklyScannedCartons")]
        public async Task<ActionResult> GetWeeklyScannedCartons()
        {
            return Ok(await _dashBoardRepository.GetWeelklyScannedCartons());

        }

        [HttpGet("gettWeeklyScannedCartonsByWH")]
        public async Task<ActionResult> GetWeeklyScannedCartonsByWH()
        {
            return Ok(await _dashBoardRepository.GetWeelklyWeeklyScannedCartonsByWH());

        }
    }
}
