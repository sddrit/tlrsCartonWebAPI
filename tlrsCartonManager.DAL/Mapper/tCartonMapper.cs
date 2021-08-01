using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Carton;
using tlrsCartonManager.DAL.Dtos.Company;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.DAL.Dtos.Location;
using tlrsCartonManager.DAL.Dtos.Menu;
using tlrsCartonManager.DAL.Dtos.MetaData;
using tlrsCartonManager.DAL.Dtos.Module;
using tlrsCartonManager.DAL.Dtos.Ownership;
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Base;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Models.DashBoard;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.DAL.Models.InvoiceProfile;
using tlrsCartonManager.DAL.Models.MetaData;
using tlrsCartonManager.DAL.Models.Ownership;
using tlrsCartonManager.DAL.Models.Pick;
using tlrsCartonManager.DAL.Models.RoleResponse;

namespace tlrsCartonManager.DAL.Mapper
{
    public class tCartonMapper:Profile
    {
        public tCartonMapper()
        {
           
            CreateMap<UserDto, User>();
            CreateMap<User,UserDto>();
            CreateMap<UserActivityTypeDto, UserActivityType>();
            CreateMap<UserActivityType, UserActivityTypeDto>();
            CreateMap<UserPasswordDto, UserPassword>();
            CreateMap<UserPassword, UserPasswordDto>();
            CreateMap<UserLoggerDto, UserLogger>();
            CreateMap<UserLogger, UserLoggerDto>();
            CreateMap<UserActivityLoggerDto, UserActivityLogger>();
            CreateMap<UserActivityLogger, UserActivityLoggerDto>();
            CreateMap<MenuRightFormUser, MenuRightFormUserAttachedDto>();
            CreateMap<MenuRightAttachedUser, MenuRightAttachedUserDto>();


            //ruv
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();         

            CreateMap<CustomerSearchDto, CustomerSearch>();
            CreateMap<CustomerSearch, CustomerSearchDto>();
         
            CreateMap<CustomerDeleteDto, Customer>();           
           
            CreateMap<CustomerSubAccountListDto, CustomerSubAccountList>();
            CreateMap<CustomerSubAccountList, CustomerSubAccountListDto>();

            CreateMap<CustomerSubAccountListDto, Customer>();
            CreateMap<Customer, CustomerSubAccountListDto>();


            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryDto>();

          

            CreateMap<CustomerMainCodeSearchDto, Customer>();
            CreateMap<Customer, CustomerMainCodeSearchDto>();

            CreateMap<CustomerAuthorizationListHeaderDto, CustomerAuthorizationListHeader>();
            CreateMap<CustomerAuthorizationListHeader, CustomerAuthorizationListHeaderDto>();           

            CreateMap<CustomerAuthorizationListDetailDto, CustomerAuthorizationListDetail>();
            CreateMap<CustomerAuthorizationListDetail, CustomerAuthorizationListDetailDto>();

            CreateMap<CustomerAuthorizationListUtdDto, CustomerAuthorizationListHeaderDto>();
            CreateMap<CustomerAuthorizationListHeaderDto, CustomerAuthorizationListUtdDto>();

            CreateMap<CustomerAuthorizationListDetailUdtDto, CustomerAuthorizationListDetailDto>();
            CreateMap<CustomerAuthorizationListDetailDto, CustomerAuthorizationListDetailUdtDto>();           

           
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<UserSerachDto, UserSearch>();
            CreateMap<UserSearch, UserSerachDto>();

            CreateMap<CartonStorageDto, CartonStorage>();
            CreateMap<CartonStorage, CartonStorageDto>();

            CreateMap<CartonStorageSearchDto, CartonStorageSearch>();
            CreateMap<CartonStorageSearch, CartonStorageSearchDto>();

            CreateMap<CartonLocationDto, CartonLocation>();
            CreateMap<CartonLocation, CartonLocationDto>();

            CreateMap<RequestSearchDto, RequestSearch>();
            CreateMap<RequestSearch, RequestSearchDto>();

            CreateMap<RequestHeaderDto, RequestHeader>();
            CreateMap<RequestHeader, RequestHeaderDto>();

            CreateMap<RequestDetailDto, RequestDetail>();
            CreateMap<RequestDetail, RequestDetailDto>();

            CreateMap<InvoiceHeaderDto, InvoiceHeader>();
            CreateMap<InvoiceHeader, InvoiceHeaderDto>();

            CreateMap<InvoiceDetailDto, InvoiceDetail>();
            CreateMap<InvoiceDetail, InvoiceDetailDto>();

            CreateMap<InvoiceSearchDto, InvoiceSearch>();
            CreateMap<InvoiceSearch, InvoiceSearchDto>();


            CreateMap<InvoiceConfirmationDto, InvoiceConfirmation>();
            CreateMap<InvoiceConfirmation, InvoiceConfirmationDto>();

            CreateMap<InvoiceConfirmationSearchDto, InvoiceConfirmationSearch>();
            CreateMap<InvoiceConfirmationSearch, InvoiceConfirmationSearchDto>();

            CreateMap<InvoiceConfirmationDetailDto, InvoiceConfirmationDetail>();
            CreateMap<InvoiceConfirmationDetail, InvoiceConfirmationDetailDto>();

            CreateMap<PickListSearchDto, PickListSearch>();
            CreateMap<PickListSearch, PickListSearchDto>();

            CreateMap<PickListHeaderDto, PickList>().ReverseMap();
            CreateMap<PickListDetailItemDto, PickList>().ReverseMap();
            CreateMap<PickListHeaderDto, ViewPickListByNo>().ReverseMap();
            CreateMap<PickListDetailItemDto, ViewPickListByNo>().ReverseMap();

            CreateMap<PickListDto, PickList>();
            CreateMap<PickList, PickListDto>();

            // CreateMap<PickList, PickListHeaderDto>();

            CreateMap<CartonOverview, CartonOverviewDto>();
            CreateMap<CartonOverviewDto, CartonOverview>();


            CreateMap<DisposalTimeFrameDto, DisposalTimeFrame>();
            CreateMap<DisposalTimeFrame, DisposalTimeFrameDto>();

            CreateMap<CustomerSearchDto, Customer>();
            CreateMap<Customer, CustomerSearchDto>();

            CreateMap<WorkOrderTypeDto, WorkOrderRequestType>();
            CreateMap<WorkOrderRequestType, WorkOrderTypeDto>();

            CreateMap<CustomerAuthorizationHeader, CustomerAuthorizationListHeader>();
            CreateMap<CustomerAuthorizationListHeader, CustomerAuthorizationHeader>();

            CreateMap<CartonOwnerShip, CartonOwnerShipDto>();
            CreateMap<CartonOwnerShipDto, CartonOwnerShip>();

            CreateMap<PickListPendingListItem, PickListDetailItemDto>();
            CreateMap<PickListDetailItemDto, PickListPendingListItem>();

            CreateMap<MobileDevice, MobileDeviceDto>().ReverseMap();
            CreateMap<WorkerDto, User>().ReverseMap();
            CreateMap<PostingTypeDto, PostingType>().ReverseMap();

            CreateMap<InvoicePosting, InvoicePostingDto>().ReverseMap();

            CreateMap<RequestOriginalDocket, RequestHeader>().ReverseMap();
            CreateMap<OriginalDocketSearchDto, OriginalDocketSearch>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<TaxEffectiveDate, TaxEffectiveDateDto>().ReverseMap();
            CreateMap<TaxType, TaxTypeDto>().ReverseMap();
            CreateMap<RequestType, RequestTypeDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<RoleResponseListItem, Role>().ReverseMap();
            CreateMap<UserModulePermission, ViewMenu>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<object, TableReturn>().ReverseMap();
            CreateMap<DocketPrintResultModel, ViewRequestSummary>().ReverseMap();
            CreateMap<DocketPrintBulkResult, DocketPrintModel>().ReverseMap();
            CreateMap<InvoicePrintModel, ViewCreatedInvoiceList>().ReverseMap();
            CreateMap<UserModulePermission, ViewModulePermission>().ReverseMap();

            CreateMap<Module, ModuleDto>().ReverseMap();           
            CreateMap<ModuleSub, SubModuleDto>()
                .ForMember(d => d.ModuleName, s => s.MapFrom(d => d.Module.Name))
                .ForMember(d => d.ModulePermissions, s => s.MapFrom(d => d.ModulePermissions.Select(p => p.PermissionId))).ReverseMap();
            CreateMap<ModulePermission, ModulePermissionDto>().ReverseMap();

            //meta data section
            CreateMap<StorageType, StorageTypeDto>().ReverseMap();
            CreateMap<BillingCycle, BillingCycleDto>().ReverseMap();
            CreateMap<Route, RouteDto>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<ReceiveType, ReceiveTypeDto>().ReverseMap();
            CreateMap<RequestType, RequestTypeDto>().ReverseMap();
            CreateMap<WorkOrderRequestType, WorkOrderTypeDto>().ReverseMap();
            CreateMap<DisposalTimeFrame, DisposalTimeFrameDto>().ReverseMap();
            CreateMap<TaxType, TaxTypeDto>().ReverseMap();
            CreateMap<PostingType, PostingTypeDto>().ReverseMap();
            CreateMap<MobileDevice, MobileDeviceDto>().ReverseMap();
            CreateMap<Module, ModuleMetaDataDto>().ReverseMap();
            CreateMap<ModuleSub, ModuleSubMetaDataDto>().ReverseMap();
               

            CreateMap<MetadataBase, StorageType>().ReverseMap();
            CreateMap<MetadataBase, BillingCycle>().ReverseMap();
            CreateMap<MetadataBase, Route>().ReverseMap();
            CreateMap<MetadataBase, ServiceCategory>().ReverseMap();
            CreateMap<MetadataBase, Department>().ReverseMap();
            CreateMap<MetadataBase, ReceiveType>().ReverseMap();
            CreateMap<MetadataBase, RequestType>().ReverseMap();
            CreateMap<MetadataBase, WorkOrderRequestType>().ReverseMap();
            CreateMap<MetadataBase, DisposalTimeFrame>().ReverseMap();
            CreateMap<MetadataBase, TaxType>().ReverseMap();
            CreateMap<MetadataBase, PostingType>().ReverseMap();
            CreateMap<MetadataBase, MobileDevice>().ReverseMap();
            CreateMap<MetadataBase, Module>()
                .ForMember(d=>d.Id, s=>s.MapFrom(d=>d.ModuleId))
                .ForMember(d => d.Name, s => s.MapFrom(d => d.Description))
                .ReverseMap();
            CreateMap<MetadataBase, ModuleSub>()
                .ForMember(d=>d.SubModuleId, s=>s.MapFrom(d=>d.Id))
                .ForMember(d => d.SubModuleName, s => s.MapFrom(d => d.Description))
                .ReverseMap();


            CreateMap<MetadataBase, StorageTypeDto>().ReverseMap();
            CreateMap<MetadataBase, BillingCycleDto>().ReverseMap();
            CreateMap<MetadataBase, RouteDto>().ReverseMap();
            CreateMap<MetadataBase, ServiceCategoryDto>().ReverseMap();
            CreateMap<MetadataBase, DepartmentDto>().ReverseMap();
            CreateMap<MetadataBase, ReceiveTypeDto>().ReverseMap();
            CreateMap<MetadataBase, RequestTypeDto>().ReverseMap();
            CreateMap<MetadataBase, WorkOrderTypeDto>().ReverseMap();
            CreateMap<MetadataBase, DisposalTimeFrameDto>().ReverseMap();
            CreateMap<MetadataBase, TaxTypeDto>().ReverseMap();
            CreateMap<MetadataBase, PostingTypeDto>().ReverseMap();
            CreateMap<MetadataBase, MobileDeviceDto>().ReverseMap();
            CreateMap<MetadataBase, ModuleMetaDataDto>()
                .ForMember(d => d.Id, s => s.MapFrom(d => d.ModuleId))
                .ForMember(d => d.Name, s => s.MapFrom(d => d.Description))
                .ReverseMap();
            CreateMap<MetadataBase, ModuleSubMetaDataDto>()
                .ForMember(d => d.SubModuleId, s => s.MapFrom(d => d.Id))
                .ForMember(d => d.SubModuleName, s => s.MapFrom(d => d.Description))
                .ReverseMap();


            CreateMap<DashBoardWeeklyWOStatusDetail, DashBoardWeeklyWOStaus>().ReverseMap();
            CreateMap<DashBoardWeeklyWOStatusDetail, DashBoardWeeklyWOStausCarton>().ReverseMap();

            CreateMap<ViewCreatedInvoiceList, InvoiceHeaderResponse>().ReverseMap();            
            CreateMap<ViewCreatedInvoiceListSub, InvoiceHeaderResponse>().ReverseMap();

            CreateMap<ViewCreatedInvoiceList, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceTemplateHeaderCustomer, InvoiceProfileHeaderModel>().ReverseMap();
            CreateMap<SupportingDocsViewModel, InvoiceTemplateSuportingDocsCustomer>()
                 .ForMember(d => d.DocId, s => s.MapFrom(d => d.Id))
                .ReverseMap();
        }


    }
}
