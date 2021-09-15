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
    public class ReminderController : Controller
    {
        private readonly IReminderManagerRepository _reminderRepository;
        public ReminderController(IReminderManagerRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        [HttpGet]
        [RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> SearchDailyCollection(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var cartonList = await _reminderRepository.SearchReminders(searchText, searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(cartonList);
        }

        [HttpGet("{requestNo}")]
        [RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetSingleSearch(string requestNo)
        {
            return Ok(await _reminderRepository.GetReminderListById(requestNo));
        }

        [HttpPut]
        [RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public IActionResult MarkDailyCollection(ReminderUpdateDto request)
        {
            return Ok( _reminderRepository.UpdateReminders(request));
        }

    }
}
