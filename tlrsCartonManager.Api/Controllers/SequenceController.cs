﻿using AutoMapper;
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
using tlrsCartonManager.DAL.Dtos.Company;
using tlrsCartonManager.Api.Util.Authorization;
using tlrsCartonManager.DAL.Models.SequenceMonthEnd;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SequenceController : Controller
    {
        private readonly ISequenceManagerRepository _sequenceRepository;
        public SequenceController(ISequenceManagerRepository sequenceRepository)
        {
            _sequenceRepository = sequenceRepository;
        }

        [HttpGet("getSequence")]
        [RmsAuthorization("Sequence", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult<Company> GetSequence()
        {
            return Ok(_sequenceRepository.GetSequenceAsync());
        }

        [HttpGet("getSequenceByCode/{code}")]
        [RmsAuthorization("Sequence", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult<Company> GetSequenceByCode(string code)
        {
            return Ok(_sequenceRepository.GetSequenceByCodeAsync(code));
        }


        [HttpPut]
        [RmsAuthorization("Sequence", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public ActionResult UpdateSequence(SequenceModel request)
        {
            return Ok(_sequenceRepository.UpdateSequence(request));
        }

        [HttpPost]
        [RmsAuthorization("Sequence", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult AddSequence(SequenceModel request)
        {
            return Ok(_sequenceRepository.AddSequence(request));

        }

    }
}
