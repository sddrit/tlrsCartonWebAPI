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
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationManagerRepository _locationRepository;

        public LocationController(ILocationManagerRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("getLocations")]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<CustomerSearchDto>> SearchLocation(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            return Ok(await _locationRepository.SearchLocation(columnValue,searchColumn,sortOrder, pageIndex, pageSize));
        }

        [HttpGet("getLocationByCode/{locationCode}")]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<LocationDto>> GetSingleSearch(string locationCode)
        {
            var locationList = await _locationRepository.GetLocationListByCode(locationCode);
            if(locationList != null)
                return Ok(locationList);
            else              
                return new JsonErrorResult(new { Message = "Location Not Found" }, HttpStatusCode.BadRequest);
        }


        [HttpGet("getLocation")]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetLocation(string locationCode)
        {
            var locationList = await _locationRepository.GetLocationByCode(locationCode);
            if (locationList != null)
                return Ok(locationList);
            else
                return new JsonErrorResult(new { Message = "Location Not Found" }, HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult AddLocation(LocationDto location)
        {
            return Ok(_locationRepository.AddLocation(location));
        }

        [HttpPut]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public ActionResult UpdateLocation(LocationDto location)
        {
            return Ok(_locationRepository.UpdateLocation(location));
        }

        [HttpDelete]
        [RmsAuthorization("Location", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public ActionResult DeleteLocation(LocationDto location)
        {
            return Ok(_locationRepository.DeleteLocation(location));

        }

    }
}
