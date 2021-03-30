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

            CreateMap<CustomerDisplayDto, Customer>();
            CreateMap<Customer, CustomerDisplayDto>();

            CreateMap<CustomerSearchDto, CustomerSearch>();
            CreateMap<CustomerSearch, CustomerSearchDto>();

            CreateMap<CustomerInsertUpdateDto, Customer>();
            CreateMap<CustomerDeleteDto, Customer>();

            CreateMap<CustomerAuthorizationListInsertDto, CustomerAuthorizationListDto>();
            CreateMap<CustomerAuthorizationListDto, CustomerAuthorizationListInsertDto>();


            CreateMap<CustomerInsertDto, CustomerInsertUpdateDto>();

            CreateMap<CustomerAuthorizationListDisplayDto, CustomerAuthorizationList>();
            CreateMap<CustomerAuthorizationList, CustomerAuthorizationListDisplayDto>();

            CreateMap<CustomerAuthorizationListDto, CustomerAuthorizationList>();
            //ruv
        }


    }
}
