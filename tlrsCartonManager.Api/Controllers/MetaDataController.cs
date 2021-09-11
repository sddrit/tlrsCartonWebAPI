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
using tlrsCartonManager.DAL.Dtos.MetaData;
using tlrsCartonManager.DAL.Dtos.Module;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : Controller
    {
        private readonly IMetadataRepository<StorageType, StorageTypeDto> _storageTypeRepository;
        private readonly IMetadataRepository<BillingCycle, BillingCycleDto> _billingCycleRepository;
        private readonly IMetadataRepository<Route, RouteDto> _routeRepository;
        private readonly IMetadataRepository<ServiceCategory, ServiceCategoryDto> _serviceRepository;
        private readonly IMetadataRepository<Department, DepartmentDto> _departmentRepository;
        private readonly IMetadataRepository<ReceiveType, ReceiveTypeDto> _receiveTypeRepository;
        private readonly IMetadataRepository<RequestType, RequestTypeDto> _requestTypeTypeManagerRepository;
        private readonly IMetadataRepository<WorkOrderRequestType, WorkOrderTypeDto> _workOrderTypeRepository;
        private readonly IMetadataRepository<DisposalTimeFrame, DisposalTimeFrameDto> _disposalTimeFrameRepository;
        private readonly IMetadataRepository<PostingType, PostingTypeDto> _postingTypeRepository;
        private readonly IMetadataRepository<TaxType, TaxTypeDto> _taxTypeManagerRepository;
        private readonly IMetadataRepository<MobileDevice, MobileDeviceDto> _mobileDeviceRepository;
        private readonly IMetadataRepository<Module, ModuleMetaDataDto> _moduleRepository;
        private readonly IMetadataRepository<ModuleSub, ModuleSubMetaDataDto> _subModuleRepository;

        private readonly IRolePermissionManagerRepository _rolePermission;


        public MetaDataController
            (
              IMetadataRepository<BillingCycle, BillingCycleDto> billingCycleRepository,
              IMetadataRepository<StorageType, StorageTypeDto> storageTypeRepository,
              IMetadataRepository<Route, RouteDto> routeRepository,
              IMetadataRepository<ServiceCategory, ServiceCategoryDto> serviceRepository,
              IMetadataRepository<Department, DepartmentDto> departmentRepository,
              IMetadataRepository<ReceiveType, ReceiveTypeDto> receiveTypeRepository,
              IMetadataRepository<RequestType, RequestTypeDto> requestTypeTypeManagerRepository,
              IMetadataRepository<WorkOrderRequestType, WorkOrderTypeDto> workOrderTypeRepository,
              IMetadataRepository<DisposalTimeFrame, DisposalTimeFrameDto> disposalTimeFrame,
              IMetadataRepository<MobileDevice, MobileDeviceDto> mobileDeviceRepository,
              IMetadataRepository<PostingType, PostingTypeDto> postingTypeRepository,
              IMetadataRepository<TaxType, TaxTypeDto> taxTypeManagerRepository,
              IMetadataRepository<Module, ModuleMetaDataDto> moduleRepository,
              IMetadataRepository<ModuleSub, ModuleSubMetaDataDto> subModuleRepository,
              IRolePermissionManagerRepository rolePermission)

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
            _postingTypeRepository = postingTypeRepository;
            _taxTypeManagerRepository = taxTypeManagerRepository;
            _requestTypeTypeManagerRepository = requestTypeTypeManagerRepository;
            _moduleRepository = moduleRepository;
            _subModuleRepository = subModuleRepository;
            _rolePermission = rolePermission;
        }

        [HttpGet]
        public async Task<IActionResult> GetMetaData()
        {


            //customer
            var serviceCategory = await _serviceRepository.GetAllMetaData();
            var route = await _routeRepository.GetAllMetaData();
            var billingCylce = await _billingCycleRepository.GetAllMetaData();

            //wo
            var receiveTypeList = await _receiveTypeRepository.GetAllMetaData();
            var requestTypeList = await _requestTypeTypeManagerRepository.GetAllMetaData();
            var workOrderTypeList = await _workOrderTypeRepository.GetAllMetaData();


            //carton
            var storageType = await _storageTypeRepository.GetAllMetaData();
            var disposalTimeFrameList = await _disposalTimeFrameRepository.GetAllMetaData();

            //company
            var departmentList = await _departmentRepository.GetAllMetaData();


            //invoice
            var postingTypeList = await _postingTypeRepository.GetAllMetaData();
            var taxTypeList = await _taxTypeManagerRepository.GetAllMetaData();

            //other
            var mobileDeviceList = await _mobileDeviceRepository.GetAllMetaData();
            var moduleList = await _moduleRepository.GetAllMetaData();           
            var subModuleList = await _rolePermission.GeModulePermissionList();



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
                postingTypeList,
                taxTypeList,
                requestTypeList,
                moduleList,
                subModuleList


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
            return Ok(await _billingCycleRepository.GetAll());
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
            return Ok(await _serviceRepository.GetAll());
        }

        [HttpGet("serviceCategory/{id}")]
        public async Task<IActionResult> GetServiceCategory(int id)
        {
            return Ok(await _serviceRepository.GetById(id));
        }

        [HttpPost("serviceCategory")]
        public async Task<IActionResult> AddServiceCategory(ServiceCategoryDto model)
        {
            return Ok(await _serviceRepository.AddItem(model));
        }

        [HttpPut("serviceCategory")]
        public async Task<IActionResult> UpdateServiceCategory(ServiceCategoryDto model)
        {
            return Ok(await _serviceRepository.EditItem(model));
        }

        [HttpDelete("serviceCategory")]
        public async Task<IActionResult> DeleteServiceCategory(int id)
        {
            await _serviceRepository.DeleteItem(id);
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

        #region request type

        [HttpGet("getAllRequestTypes")]
        public async Task<IActionResult> GetAllRequestTypes()
        {
            return Ok(await _requestTypeTypeManagerRepository.GetAll());
        }

        [HttpGet("requestType/{id}")]
        public async Task<IActionResult> GetRequestType(int id)
        {
            return Ok(await _requestTypeTypeManagerRepository.GetById(id));
        }

        [HttpPost("requestType")]
        public async Task<IActionResult> AddRequestType(RequestTypeDto model)
        {
            return Ok(await _requestTypeTypeManagerRepository.AddItem(model));
        }

        [HttpPut("requestType")]
        public async Task<IActionResult> UpdateRequestType(RequestTypeDto model)
        {
            return Ok(await _requestTypeTypeManagerRepository.EditItem(model));
        }

        [HttpDelete("requestType")]
        public async Task<IActionResult> DeleteRequestType(int id)
        {
            await _requestTypeTypeManagerRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Wo type

        [HttpGet("getAllWorkOrderTypes")]
        public async Task<IActionResult> GetAllWorkOrderTypes()
        {
            return Ok(await _workOrderTypeRepository.GetAll());
        }

        [HttpGet("workOrderType/{id}")]
        public async Task<IActionResult> GetWorkOrderType(int id)
        {
            return Ok(await _workOrderTypeRepository.GetById(id));
        }

        [HttpPost("workOrderType")]
        public async Task<IActionResult> AddWorkOrderType(WorkOrderTypeDto model)
        {
            model.ShowInventoryReport = true;
            return Ok(await _workOrderTypeRepository.AddItem(model));
        }

        [HttpPut("workOrderType")]
        public async Task<IActionResult> UpdateWorkOrderType(WorkOrderTypeDto model)
        {
            //model.ShowInventoryReport = true;
            return Ok(await _workOrderTypeRepository.EditItem(model));
        }

        [HttpDelete("workOrderType")]
        public async Task<IActionResult> DeleteWorkOrderType(int id)
        {
            await _workOrderTypeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Disposasl time frame

        [HttpGet("getAllDisposalTimeFrames")]
        public async Task<IActionResult> GetAllDisposalTimeFrames()
        {
            return Ok(await _disposalTimeFrameRepository.GetAll());
        }

        [HttpGet("disposalTimeFrame/{id}")]
        public async Task<IActionResult> GetDisposalTimeFrame(int id)
        {
            return Ok(await _disposalTimeFrameRepository.GetById(id));
        }

        [HttpPost("disposalTimeFrame")]
        public async Task<IActionResult> AddDisposalTimeFrame(DisposalTimeFrameDto model)
        {
            return Ok(await _disposalTimeFrameRepository.AddItem(model));
        }

        [HttpPut("disposalTimeFrame")]
        public async Task<IActionResult> UpdateDisposalTimeFrame(DisposalTimeFrameDto model)
        {
            return Ok(await _disposalTimeFrameRepository.EditItem(model));
        }

        [HttpDelete("disposalTimeFrame")]
        public async Task<IActionResult> DeleteDisposalTimeFrame(int id)
        {
            await _disposalTimeFrameRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Posting types

        [HttpGet("getAllPostingTypes")]
        public async Task<IActionResult> GetAllPostingTypes()
        {
            return Ok(await _postingTypeRepository.GetAll());
        }

        [HttpGet("postingType/{id}")]
        public async Task<IActionResult> GetPostingType(int id)
        {
            return Ok(await _postingTypeRepository.GetById(id));
        }

        [HttpPost("postingType")]
        public async Task<IActionResult> AddPostingType(PostingTypeDto model)
        {
            return Ok(await _postingTypeRepository.AddItem(model));
        }

        [HttpPut("postingType")]
        public async Task<IActionResult> UpdatePostingType(PostingTypeDto model)
        {
            return Ok(await _postingTypeRepository.EditItem(model));
        }

        [HttpDelete("postingType")]
        public async Task<IActionResult> DeletePostingType(int id)
        {
            await _postingTypeRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Tax types

        [HttpGet("getAllTaxTypes")]
        public async Task<IActionResult> GetAllTaxTypes()
        {
            return Ok(await _taxTypeManagerRepository.GetAll());
        }

        [HttpGet("taxType/{id}")]
        public async Task<IActionResult> GetTaxType(int id)
        {
            return Ok(await _taxTypeManagerRepository.GetById(id));
        }

        [HttpPost("taxType")]
        public async Task<IActionResult> AddTaxType(TaxTypeDto model)
        {
            return Ok(await _taxTypeManagerRepository.AddItem(model));
        }

        [HttpPut("taxType")]
        public async Task<IActionResult> UpdateTaxType(TaxTypeDto model)
        {
            return Ok(await _taxTypeManagerRepository.EditItem(model));
        }

        [HttpDelete("taxType")]
        public async Task<IActionResult> DeleteTaxType(int id)
        {
            await _taxTypeManagerRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Mobile Devices

        [HttpGet("getAllMobileDevices")]
        public async Task<IActionResult> GetAllMobileDevices()
        {
            return Ok(await _mobileDeviceRepository.GetAll());
        }

        [HttpGet("mobileDevice/{id}")]
        public async Task<IActionResult> GetMobileDevice(int id)
        {
            return Ok(await _mobileDeviceRepository.GetById(id));
        }

        [HttpPost("mobileDevice")]
        public async Task<IActionResult> AddMobileDevice(MobileDeviceDto model)
        {
            return Ok(await _mobileDeviceRepository.AddItem(model));
        }

        [HttpPut("mobileDevice")]
        public async Task<IActionResult> UpdateMobileDevice(MobileDeviceDto model)
        {
            return Ok(await _mobileDeviceRepository.EditItem(model));
        }

        [HttpDelete("mobileDevice")]
        public async Task<IActionResult> DeleteMobileDevice(int id)
        {
            await _mobileDeviceRepository.DeleteItem(id);
            return Ok();
        }
        #endregion

        #region Modules

        [HttpGet("getAllModules")]
        public async Task<IActionResult> GetAllModuels()
        {
            return Ok(await _moduleRepository.GetAll());
        }

        [HttpGet("module/{id}")]
        public async Task<IActionResult> GetModuleDevice(int id)
        {
            return Ok(await _moduleRepository.GetById(id));
        }

        [HttpPost("module")]
        public async Task<IActionResult> AddModuleDevice(ModuleMetaDataDto model)
        {
            return Ok(await _moduleRepository.AddItem(model));
        }

        [HttpPut("module")]
        public async Task<IActionResult> UpdateModuleDevice(ModuleMetaDataDto model)
        {
            return Ok(await _moduleRepository.EditItem(model));
        }

        [HttpDelete("module")]
        public async Task<IActionResult> DeleteModuleDevice(int id)
        {
            await _moduleRepository.DeleteItem(id);
            return Ok();
        }
        #endregion


        #region SubModules

        [HttpGet("getAllSubModules")]
        public async Task<IActionResult> GetAllSubModuels()
        {
            return Ok(await _subModuleRepository.GetAll());
        }

        [HttpGet("subModule/{id}")]
        public async Task<IActionResult> GetSubModuleDevice(int id)
        {
            return Ok(await _subModuleRepository.GetById(id));
        }

        [HttpPost("subModule")]
        public async Task<IActionResult> AddSubModuleDevice(ModuleSubMetaDataDto model)
        {
            return Ok(await _subModuleRepository.AddItem(model));
        }

        [HttpPut("subModule")]
        public async Task<IActionResult> UpdateSubModuleDevice(ModuleSubMetaDataDto model)
        {
            return Ok(await _subModuleRepository.EditItem(model));
        }

        [HttpDelete("subModule")]
        public async Task<IActionResult> DeleteSubModuleDevice(int id)
        {
            await _subModuleRepository.DeleteItem(id);
            return Ok();
        }
        #endregion
    }
}
