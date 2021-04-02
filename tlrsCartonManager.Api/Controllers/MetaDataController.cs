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
    public class MetaDataController : Controller
    {
        private readonly IBillingCycleManagerRepository _billingCycleRepository;
        private readonly IRouteManagerRepository _routeRepository;
        private readonly IServiceCategoryManagerRepository _serviceRepository;

        public MetaDataController(IBillingCycleManagerRepository billingCycleRepository, IRouteManagerRepository routeRepository,
            IServiceCategoryManagerRepository serviceRepository)
        {
            _billingCycleRepository = billingCycleRepository;
            _routeRepository = routeRepository;
            _serviceRepository = serviceRepository;
        }       

        [HttpGet("BillingCycle")]
        public async Task<ActionResult<BillingCycleDto>> GetBillingList()
        {
            var bcList = await _billingCycleRepository.GetBillingList();
            if(bcList != null)
                return Json(bcList);
            else
                return Json("Not Found");

        }
        [HttpGet("Route")]
        public async Task<ActionResult<RouteDto>> GetCustomerList()
        {
            var routeList = await _routeRepository.GetRouteList();
            if (routeList != null)
                return Ok(routeList);
            else
                return Json("Not Found");

        }
        [HttpGet("ServiceCategory")]
        public async Task<ActionResult<ServiceCategoryDto>> GetServiceList()
        {
            var seviceList = await _serviceRepository.GetServiceList();
            if (seviceList != null)
                return Ok(seviceList);
            else
                return Json("Not Found");

        }
    }
}
