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
using tlrsCartonManager.DAL.Models.MetaData;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : Controller
    {    
       
        
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
        private readonly IMetadataRepository<Department, DepartmentDto> _departmentRepository;
        private readonly IMetadataRepository<ReceiveType, ReceiveTypeDto> _receiveTypeRepository;

        public MetaDataController
            (
              IMetadataRepository<BillingCycle, BillingCycleDto> billingCycleRepository,
              IMetadataRepository<StorageType, StorageTypeDto> storageTypeRepository,
              IMetadataRepository<Route, RouteDto> routeRepository,
              IMetadataRepository<ServiceCategory, ServiceCategoryDto> serviceRepository,
              IMetadataRepository<Department, DepartmentDto> departmentRepository,
              IMetadataRepository<ReceiveType, ReceiveTypeDto> receiveTypeRepository,
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
        public async Task<IActionResult> GetMetaData()
        {
            var billingCylce = await _billingCycleRepository.GetAllMetaData();
            var storageType = await _storageTypeRepository.GetAllMetaData();
            var route = await _routeRepository.GetAllMetaData();
            var serviceCategory = await _serviceRepository.GetAllMetaData();    
            var departmentList = await _departmentRepository.GetAllMetaData();
            var receiveTypeList = await _receiveTypeRepository.GetAllMetaData();

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

        [HttpGet("getAllStorageTypes")]
        public async Task<IActionResult> GetAllStorageType()
        {
            return Ok(await _storageTypeRepository.GetAll());           
        }

        [HttpGet("storageType/{id}")]
        public async Task<IActionResult> GetStorageType(int id)
        {
            return Ok(await _storageTypeRepository.GetById(id));           
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

        [HttpGet("getAllBillingCycles")]
        public async Task<IActionResult> GetAllBillingCycle()
        {
            return Ok( await _billingCycleRepository.GetAll());            
        }

        [HttpGet("billingCycle/{id}")]
        public async Task<IActionResult> GetBillingCycle(int id)
        {
            return Ok(await _billingCycleRepository.GetById(id));           
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

        [HttpGet("getAllRoutes")]
        public async Task<IActionResult> GetAllRoutes()
        {
            return Ok(await _routeRepository.GetAll());            
        }

        [HttpGet("route/{id}")]
        public async Task<IActionResult> GetRoute(int id)
        {
            return Ok(await _routeRepository.GetById(id));           
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

        [HttpGet("getAllServiceCategories")]
        public async Task<IActionResult> GetAllServiceCategory()
        {
            return Ok(await _routeRepository.GetAll());           
        }

        [HttpGet("serviceCategory/{id}")]
        public async Task<IActionResult> GetServiceCategory(int id)
        {
            return Ok(await _routeRepository.GetById(id));           
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

        #region Department

        [HttpGet("getAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _departmentRepository.GetAll());
        }

        [HttpGet("department/{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            return Ok(await _departmentRepository.GetById(id));
        }

        [HttpPost("department")]
        public async Task<IActionResult> AddDDepartment(DepartmentDto model)
        {
            return Ok(await _departmentRepository.AddItem(model));
        }

        [HttpPut("department")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto model)
        {
            return Ok(await _departmentRepository.EditItem(model));
        }

        [HttpDelete("department")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region receive type

        [HttpGet("getAllReceiveTypes")]
        public async Task<IActionResult> GetAllReceiveTypes()
        {
            return Ok(await _receiveTypeRepository.GetAll());
        }

        [HttpGet("receiveType/{id}")]
        public async Task<IActionResult> GetReceiveType(int id)
        {
            return Ok(await _receiveTypeRepository.GetById(id));
        }

        [HttpPost("receiveType")]
        public async Task<IActionResult> AddReceiveType(ReceiveTypeDto model)
        {
            return Ok(await _receiveTypeRepository.AddItem(model));
        }

        [HttpPut("receiveType")]
        public async Task<IActionResult> UpdateReceiveType(ReceiveTypeDto model)
        {
            return Ok(await _receiveTypeRepository.EditItem(model));
        }

        [HttpDelete("receiveType")]
        public async Task<IActionResult> DeleteReceiveType(int id)
        {
            await _receiveTypeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

    }
}
