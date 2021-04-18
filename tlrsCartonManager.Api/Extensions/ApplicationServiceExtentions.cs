using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Mapper;

namespace tlrsCartonManager.Api.Extensions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<tlrmCartonContext>
            (options => options.UseSqlServer(config.GetConnectionString("tlrmCartonConnection")));
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddAutoMapper(typeof(tlrmCartonContext).Assembly);
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<IUserPasswordManagerRepository, UserPasswordManagerRepository>();
            services.AddScoped<ITokenServicesRepository, TokenServicesRepository>();

            services.AddScoped<ICustomerManagerRepository, CustomerManagerRepository>();
            services.AddScoped<IRouteManagerRepository, RouteManagerRepository>();
            services.AddScoped<IServiceCategoryManagerRepository, ServiceCategoryManagerRepository>();
            services.AddScoped<IBillingCycleManagerRepository, BillingCycleManagerRepository>();
            services.AddScoped<IStorageTypeManagerRepository, StorageTypeManagerRepository>();
            services.AddScoped<IRoleManagerRepository, RoleManagerRepository>();
            services.AddScoped<ICartonStorageManagerRepository, CartonStorageManagerRepository>();

            services.AddScoped<ISearchManagerRepository, SearchManagerRepository >();
            services.AddScoped<IRequestManagerRepository, RequestManagerRepository>();
            services.AddScoped<IInvoiceManagerRepository, InvoiceManagerRepository>();
            services.AddScoped<IPickListManagerRepository ,PickListManagerRepository>();
            services.AddAutoMapper(typeof(tlrmCartonContext).Assembly);
            return services;
        }
    }
}
