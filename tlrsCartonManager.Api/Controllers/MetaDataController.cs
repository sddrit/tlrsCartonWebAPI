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
        private readonly IStorageTypeManagerRepository _cartonTypeRepository;
        private readonly IRoleManagerRepository _roleRepository;
        private readonly IReceiveTypeManagerRepository _receiveTypeRepository;
        private readonly IDisposalTimeFrameManagerRepository _disposalTimeFrameRepository;
        private readonly IWorkOrderTypeManagerRepository _workOrderType;
        private readonly IMobileDeviceManagerRepository _mobileDevice;



        public MetaDataController
            (
            IBillingCycleManagerRepository billingCycleRepository, IRouteManagerRepository routeRepository,
            IServiceCategoryManagerRepository serviceRepository, IStorageTypeManagerRepository cartonTypeRepository,
            IRoleManagerRepository roleRepository, IReceiveTypeManagerRepository receiveTypeRepository,
            IDisposalTimeFrameManagerRepository disposalTimeFrame, IWorkOrderTypeManagerRepository workOrderType,
            IMobileDeviceManagerRepository mobileDevice

            )
        {
            _billingCycleRepository = billingCycleRepository;
            _routeRepository = routeRepository;
            _serviceRepository = serviceRepository;
            _cartonTypeRepository = cartonTypeRepository;
            _roleRepository = roleRepository;
            _receiveTypeRepository = receiveTypeRepository;
            _disposalTimeFrameRepository = disposalTimeFrame;
            _workOrderType = workOrderType;
            _mobileDevice = mobileDevice;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerMetaData()
        {
            var billingCylce = await _billingCycleRepository.GetBillingList();
            var route = await _routeRepository.GetRouteList();
            var serviceCategory = await _serviceRepository.GetServiceList();
            var storageType = await _cartonTypeRepository.GetCartonTypeList();
            var roleList = await _roleRepository.GetRoleList();
            var receiveTypeList = await _receiveTypeRepository.GetReceiveTypeList();
            var disposalTimeFrameList = await _disposalTimeFrameRepository.GetDisposalTimeFrameList();
            var workOrderTypeList= await _workOrderType.GetWoTypeList();
            var mobileDeviceList = await _mobileDevice.GetMobileDeviceList();
            return Ok(
            new
            {
                billingCylce,
                route,
                serviceCategory,
                storageType,
                roleList,
                receiveTypeList,
                disposalTimeFrameList,
                workOrderTypeList,
                mobileDeviceList
            });
        }

        [HttpGet("StorageType")]
        public async Task<IActionResult> GetCartonMetaData()
        {
            var storageType = await _cartonTypeRepository.GetCartonTypeList();
            return Ok(
            new
            {
                storageType
            });
        }

        [HttpPost("StorageType")]
        public async Task<IActionResult> AddCartonType(StorageTypeDto cartonType)
        {
            await _cartonTypeRepository.AddCartonType(cartonType);
            return new JsonErrorResult(new { Message = "Carton Type Created" }, HttpStatusCode.OK);
        }

        [HttpPut("StorageType")]
        public async Task<IActionResult> UpdateCartonType(StorageTypeDto cartonType)
        {
            await _cartonTypeRepository.UpdateCartonType(cartonType);
            return new JsonErrorResult(new { Message = "Carton Type Updated" }, HttpStatusCode.OK);
        }
        [HttpDelete("StorageType")]
        public async Task<IActionResult> DeleteCartonType(int cartonTypeId)
        {
            await _cartonTypeRepository.DeleteCartonType(cartonTypeId);
            return new JsonErrorResult(new { Message = "Carton Type Deleted" }, HttpStatusCode.OK);
        }

    }
}
