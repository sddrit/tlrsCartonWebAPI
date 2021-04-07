using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Models;

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

            CreateMap<RouteDto, Route>();
            CreateMap<Route, RouteDto>();
            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryDto>();

            CreateMap<BillingCycleDto, BillingCycle>();
            CreateMap<BillingCycle, BillingCycleDto>();

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

            CreateMap<CartonType, CartonTypeDto>();
            CreateMap<CartonTypeDto, CartonType>();

            CreateMap<SlabTypeHeader, SlabTypeHeaderDto>();
            CreateMap<SlabTypeHeaderDto, SlabTypeHeader>();

            CreateMap<SlabTypeDetail, SlabTypeDetailDto>();
            CreateMap<SlabTypeDetailDto, SlabTypeDetail>();

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
            //ruv
        }


    }
}
