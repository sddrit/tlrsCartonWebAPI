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
        private readonly IMetadataRepository<Route, RouteDto>_routeRepository;
        private readonly IMetadataRepository<ServiceCategory, ServiceCategoryDto> _serviceRepository;
        public MetaDataController
            (
              IMetadataRepository<BillingCycle, BillingCycleDto> billingCycleRepository,
              IMetadataRepository<StorageType, StorageTypeDto> storageTypeRepository,
              IMetadataRepository<Route, RouteDto> routeRepository,
              IMetadataRepository<ServiceCategory, ServiceCategoryDto> serviceRepository,
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
            var route = await _routeRepository.GetAll();
            var serviceCategory = await _serviceRepository.GetAll();    
            

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
            var storageTypes = await _storageTypeRepository.GetById(id);
            return Ok(
            new
            {
                storageTypes
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
            var billingCycles = await _billingCycleRepository.GetById(id);
            return Ok(
            new
            {
                billingCycles
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

        #region route
        [HttpGet("route/{id}")]
        public async Task<IActionResult> GetRoute(int id)
        {
            var routes = await _routeRepository.GetById(id);
            return Ok(
            new
            {
                routes
            });
        }

        [HttpPost("route")]
        public async Task<IActionResult> AddRoute(RouteDto model)
        {
            return Ok(await _routeRepository.AddItem(model));
        }

        [HttpPut("route")]
        public async Task<IActionResult> UpdateRoute(RouteDto model)
        {
            return Ok(await _routeRepository.EditItem(model));
        }

        [HttpDelete("route")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            await _routeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region ServiceCategory
        [HttpGet("serviceCategory/{id}")]
        public async Task<IActionResult> GetServiceCategory(int id)
        {
            var serviceCategories = await _routeRepository.GetById(id);
            return Ok(
            new
            {
                serviceCategories
            });
        }

        [HttpPost("serviceCategory")]
        public async Task<IActionResult> AddServiceCategory(RouteDto model)
        {
            return Ok(await _routeRepository.AddItem(model));
        }

        [HttpPut("serviceCategory")]
        public async Task<IActionResult> UpdateServiceCategory(RouteDto model)
        {
            return Ok(await _routeRepository.EditItem(model));
        }

        [HttpDelete("serviceCategory")]
        public async Task<IActionResult> DeleteServiceCategory(int id)
        {
            await _routeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

    }
}
