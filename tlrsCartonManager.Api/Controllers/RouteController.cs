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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : Controller
    {
        private readonly IRouteManagerRepository _routeRepository;

        public RouteController(IRouteManagerRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }       

        [HttpGet]
        public async Task<ActionResult<RouteDto>> GetCustomerList()
        {
            var routeList = await _routeRepository.GetRouteList();
            if(routeList != null)
                return Ok(routeList);
            else
                return Json("Not Found");

        }
       
    }
}
