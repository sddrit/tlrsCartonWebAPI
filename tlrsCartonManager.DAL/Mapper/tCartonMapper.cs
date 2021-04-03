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

            CreateMap<CustomerInsertUpdateDto, Customer>();
            CreateMap<CustomerDeleteDto, Customer>(); 
            
            //CreateMap<CustomerInsertDto, CustomerInsertUpdateDto>();          
           
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

           

            //CreateMap<CustomerAuthorizationListInsertDto, CustomerAuthorizationListHeaderDto>();
            //CreateMap<CustomerAuthorizationListHeaderDto, CustomerAuthorizationListInsertDto>();

            //CreateMap<CustomerAuthorizationListHeaderDto, Customer>();
            //CreateMap<Customer , CustomerAuthorizationListHeaderDto>();

            CreateMap<CustomerAuthorizationListHeaderDto, CustomerAuthorizationListHeader>();
            CreateMap<CustomerAuthorizationListHeader, CustomerAuthorizationListHeaderDto>();

            //CreateMap<CustomerAuthorizationListDetailDto, Customer>();
            //CreateMap<Customer, CustomerAuthorizationListDetailDto>();

            CreateMap<CustomerAuthorizationListDetailDto, CustomerAuthorizationListDetail>();
            CreateMap<CustomerAuthorizationListDetail, CustomerAuthorizationListDetailDto>();


           
            //ruv
        }


    }
}
