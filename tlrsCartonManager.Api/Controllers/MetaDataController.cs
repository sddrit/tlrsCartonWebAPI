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
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : Controller
    {

        private readonly IRouteManagerRepository _routeRepository;
        private readonly IServiceCategoryManagerRepository _serviceRepository;
        private readonly IDepartmentManagerRepository _departmentRepository;
        private readonly IReceiveTypeManagerRepository _receiveTypeRepository;
        private readonly IDisposalTimeFrameManagerRepository _disposalTimeFrameRepository;
        private readonly IWorkOrderTypeManagerRepository _workOrderTypeRepository;
        private readonly IMobileDeviceManagerRepository _mobileDeviceRepository;
        private readonly IUserManagerRepository _workerRepository;
        private readonly IPostingTypeManagerRepository _postingTypeRepository;
        private readonly ITaxTypeManagerRepository _taxTypeManagerRepository;
        private readonly IRequestTypeManagerRepository _requestTypeTypeManagerRepository;
        private readonly IRolePermissionManagerRepository _rolePermissionManagerRepository;



        private readonly IMetadataRepository<StorageType, StorageTypeDto> _storageTypeRepository;
        private readonly IMetadataRepository<BillingCycle, BillingCycleDto> _billingCycleRepository;

        public MetaDataController
            (
              IMetadataRepository<BillingCycle, BillingCycleDto> billingCycleRepository,
              IMetadataRepository<StorageType, StorageTypeDto> storageTypeRepository,
              IRouteManagerRepository routeRepository,
              IServiceCategoryManagerRepository serviceRepository,
              IDepartmentManagerRepository departmentRepository, IReceiveTypeManagerRepository receiveTypeRepository,
              IDisposalTimeFrameManagerRepository disposalTimeFrame, IWorkOrderTypeManagerRepository workOrderTypeRepository,
              IMobileDeviceManagerRepository mobileDeviceRepository, IUserManagerRepository workerRepository,
            IPostingTypeManagerRepository postingTypeRepository, ITaxTypeManagerRepository taxTypeManagerRepository,
            IRequestTypeManagerRepository requestTypeTypeManagerRepository, IRolePermissionManagerRepository rolePermissionManagerRepository)
        {
            _billingCycleRepository = billingCycleRepository;
            _routeRepository = routeRepository;
            _serviceRepository = serviceRepository;
            _storageTypeRepository = storageTypeRepository;
            _departmentRepository = departmentRepository;
            _receiveTypeRepository = receiveTypeRepository;
            _disposalTimeFrameRepository = disposalTimeFrame;
            _workOrderTypeRepository = workOrderTypeRepository;
            _mobileDeviceRepository = mobileDeviceRepository;
            _workerRepository = workerRepository;
            _postingTypeRepository = postingTypeRepository;
            _taxTypeManagerRepository = taxTypeManagerRepository;
            _requestTypeTypeManagerRepository = requestTypeTypeManagerRepository;
            _rolePermissionManagerRepository = rolePermissionManagerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerMetaData()
        {
            var billingCylce = await _billingCycleRepository.GetAll();
            var storageType = await _storageTypeRepository.GetAll();



            var route = await _routeRepository.GetRouteList();
            var serviceCategory = await _serviceRepository.GetServiceList();            
            var departmentList = await _departmentRepository.GetDepartmentList();
            var receiveTypeList = await _receiveTypeRepository.GetReceiveTypeList();
            var disposalTimeFrameList = await _disposalTimeFrameRepository.GetDisposalTimeFrameList();
            var workOrderTypeList = await _workOrderTypeRepository.GetWoTypeList();
            var mobileDeviceList = await _mobileDeviceRepository.GetMobileDeviceList();
            var workerList = await _workerRepository.GetWorkersList();
            var postingTypeList = await _postingTypeRepository.GetPostingTypeList();
            var taxTypeList = await _taxTypeManagerRepository.GetTaxTypeList();
            var requestTypeList = await _requestTypeTypeManagerRepository.GetRequestTypeList();
            var modulePermissionList = await _rolePermissionManagerRepository.GeModulePermissionList();

            return Ok(
            new
            {
                billingCylce,
                route,
                serviceCategory,
                storageType,
                departmentList,
                receiveTypeList,
                disposalTimeFrameList,
                workOrderTypeList,
                mobileDeviceList,
                workerList,
                postingTypeList,
                taxTypeList,
                requestTypeList,
                modulePermissionList

            });
        }

        #region storage types
        [HttpGet("storageType/{id}")]
        public async Task<IActionResult> GetStorageType(int id)
        {
            var storageType = await _storageTypeRepository.GetById(id);
            return Ok(
            new
            {
                storageType
            });
        }

        [HttpPost("storageType")]
        public async Task<IActionResult> AddStorageType(StorageTypeDto cartonType)
        {
            return Ok(await _storageTypeRepository.AddItem(cartonType));
        }

        [HttpPut("storageType")]
        public async Task<IActionResult> UpdateStorageType(StorageTypeDto cartonType)
        {
            return Ok(await _storageTypeRepository.EditItem(cartonType));
        }

        [HttpDelete("storageType")]
        public async Task<IActionResult> DeleteStorageTypeAsync(int id)
        {
            await _storageTypeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region billing Cycle
        [HttpGet("billingcycle/{id}")]
        public async Task<IActionResult> GetBillingCycle(int id)
        {
            var billingCycle = await _billingCycleRepository.GetById(id);
            return Ok(
            new
            {
                billingCycle
            });
        }

        [HttpPost("billingCycle")]
        public async Task<IActionResult> AddBillingCycle(BillingCycleDto model)
        {
            return Ok(await _billingCycleRepository.AddItem(model));
        }

        [HttpPut("billingCycle")]
        public async Task<IActionResult> UpdateBillingCycle(BillingCycleDto model)
        {
            return Ok(await _billingCycleRepository.EditItem(model));
        }

        [HttpDelete("billingCycle")]
        public async Task<IActionResult> DeleteBillingCycle(int id)
        {
            await _billingCycleRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

    }
}
