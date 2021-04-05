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
using tlrsCartonManager.Api.Error;
using System.Net;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : Controller
    {
        private readonly IBillingCycleManagerRepository _billingCycleRepository;
        private readonly IRouteManagerRepository _routeRepository;
        private readonly IServiceCategoryManagerRepository _serviceRepository;
        private readonly ICartonTypeManagerRepository _cartonTypeRepository;
        private readonly IRoleManagerRepository _roleRepository;

        public MetaDataController
            (
            IBillingCycleManagerRepository billingCycleRepository, IRouteManagerRepository routeRepository,
            IServiceCategoryManagerRepository serviceRepository, ICartonTypeManagerRepository cartonTypeRepository,
            IRoleManagerRepository roleRepository
            
            )
        {
            _billingCycleRepository = billingCycleRepository;
            _routeRepository = routeRepository;
            _serviceRepository = serviceRepository;
            _cartonTypeRepository = cartonTypeRepository;
            _roleRepository = roleRepository;
        }       

        [HttpGet("getMetaData")]
        public async Task<IActionResult> GetCustomerMetaData()
        {
            var billingCylce = await _billingCycleRepository.GetBillingList();
            var route = await _routeRepository.GetRouteList();
            var serviceCategory = await _serviceRepository.GetServiceList();
            var cartonType = await _cartonTypeRepository.GetCartonTypeList();
            var roleList = await _roleRepository.GetRoleList();
            return Ok(
            new
            {
                billingCylce,
                route,
                serviceCategory,
                cartonType,
                roleList
            });  
        }

        [HttpGet("getCartonType")]
        public async Task<IActionResult> GetCartonMetaData()
        {            
            var cartonType = await _cartonTypeRepository.GetCartonTypeList();
            return Ok(
            new
            {
                cartonType
            });
        }

        [HttpPost("addCartonType")]
        public async Task<IActionResult> AddCartonType(CartonTypeDto cartonType)
        {
            await _cartonTypeRepository.AddCartonType(cartonType);
            return new JsonErrorResult(new { Message = "Carton Type Created" }, HttpStatusCode.OK);
        }

        [HttpPost("updateCartonType")]
        public async Task<IActionResult> UpdateCartonType(CartonTypeDto cartonType)
        {
            await _cartonTypeRepository.UpdateCartonType(cartonType);
            return new JsonErrorResult(new { Message = "Carton Type Updated" }, HttpStatusCode.OK);
        }
        [HttpDelete("deleteCartonType")]
        public async Task<IActionResult> DeleteCartonType(int cartonTypeId)
        {
            await _cartonTypeRepository.DeleteCartonType(cartonTypeId);
            return new JsonErrorResult(new { Message = "Carton Type Deleted" }, HttpStatusCode.OK);
        }

    }
}
