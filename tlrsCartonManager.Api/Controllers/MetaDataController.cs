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

        public MetaDataController(IBillingCycleManagerRepository billingCycleRepository, IRouteManagerRepository routeRepository,IServiceCategoryManagerRepository serviceRepository)
        {
            _billingCycleRepository = billingCycleRepository;
            _routeRepository = routeRepository;
            _serviceRepository = serviceRepository;
        }       

        [HttpGet("getCustomerMetaData")]
        public async Task<ActionResult<BillingCycleDto>> GetBillingList()
        {
            var billingCylce = await _billingCycleRepository.GetBillingList();
            var route = await _routeRepository.GetRouteList();
            var serviceCategory = await _serviceRepository.GetServiceList();
            return Ok(
            new
            {
                billingCylce,
                route,
                serviceCategory
            });  
        }         
    }
}
