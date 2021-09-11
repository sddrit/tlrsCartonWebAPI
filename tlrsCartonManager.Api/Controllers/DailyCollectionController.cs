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
using tlrsCartonManager.Api.Util.Authorization;
using tlrsCartonManager.DAL.Dtos.DailyCollectionMark;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DailyCollectionController : Controller
    {
        private readonly IMarkDailyCollectionManagerRepository _dailyCollectionRepository;
        public DailyCollectionController(IMarkDailyCollectionManagerRepository dailyCollectionRepository)
        {
            _dailyCollectionRepository = dailyCollectionRepository;
        }

        [HttpGet]
        [RmsAuthorization("Mark Daily Collection", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> SearchDailyCollection(string searchText, int pageIndex, int pageSize)
        {
            var cartonList = await _dailyCollectionRepository.SearchDailyCollection(searchText, pageIndex, pageSize);
            return Ok(cartonList);
        }

        [HttpGet("{requestNo}")]
        [RmsAuthorization("Mark Daily Collection", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetSingleSearch(string requestNo)
        {
            return Ok(await _dailyCollectionRepository.GetDailyCollectionById(requestNo));
        }

        [HttpPut]
        [RmsAuthorization("Mark Daily Collection", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public IActionResult MarkDailyCollection(DailyCollectionMarkUpdateDto request)
        {
            return Ok( _dailyCollectionRepository.UpdateDailyCollection(request));
        }

    }
}
