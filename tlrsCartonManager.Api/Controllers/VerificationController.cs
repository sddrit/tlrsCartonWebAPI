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
using tlrsCartonManager.DAL.Models.Verification;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VerificationController : Controller
    {
        private readonly IVerficationManagerRepository _verificationRepository;
        public VerificationController(IVerficationManagerRepository verificationRepository)
        {
            _verificationRepository = verificationRepository;
        }     
      

        [HttpGet("getInvalidScans")]
        //[RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> InvalidScans(string requestNo)
        {
            return Ok(await _verificationRepository.GetInvalidScans(requestNo));
        }
        [HttpGet("getVerified")]
        //[RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetVerified(string requestNo)
        {
            return Ok(await _verificationRepository.GetVerified(requestNo));
        }

        [HttpPost]
        //[RmsAuthorization("Add Reminders", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<IActionResult> UpdatePickVerificationAsync(VerificationPickModel request)
        {
            return Ok(await _verificationRepository.UpdateAndGetCartons(request));
        }

    }
}
