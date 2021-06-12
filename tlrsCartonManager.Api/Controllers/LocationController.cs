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
using tlrsCartonManager.DAL.Utility;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Dtos.Location;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationManagerRepository _locationRepository;

        public LocationController(ILocationManagerRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

       

        [HttpGet("getLocationByCode/{locationCode}")]
        public async Task<ActionResult<LocationDto>> GetSingleSearch(string locationCode)
        {
            var locationList = await _locationRepository.GetLocationListByCode(locationCode);
            if(locationList != null)
                return Ok(locationList);
            else              
                return new JsonErrorResult(new { Message = "Location Not Found" }, HttpStatusCode.BadRequest);
        }
       
       
       
    }
}
