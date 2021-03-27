using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Mapper;

namespace tlrsCartonManager.DAL.Extensions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<tlrmCartonContext>
            (options => options.UseSqlServer(config.GetConnectionString("tlrmCartonConnection")));
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddAutoMapper(typeof(tlrmCartonContext).Assembly);

            //services.AddDbContext<tlrmCartonContext>
            //(options => options.UseSqlServer(config.GetConnectionString("tlrmCartonConnection")));
            //services.AddScoped<IUserManagerRepository, UserManagerRepository>();


            //services.AddDbContext<tlrmCartonContext>
            // (options => options.UseSqlServer(config.GetConnectionString("tlrmCartonConnection")));

            //services.AddScoped<IUserManagerRepository, UserManagerRepository>();

            //services.AddScoped<IUserActivityTypeManagerRepository, UserActivityTypeManagerRepository>();
            //services.AddScoped<IUserPasswordManagerRepository, UserPasswordManagerRepository>();
            //services.AddScoped<IUserLoggerManagerRepository, UserLoggerManagerRepository>();
            //services.AddScoped<IUserActivityLoggerManagerRepository, UserActivityLoggerManagerRepository>();

            //services.AddScoped<IUserPasswordManagerRepository, UserPasswordManagerRepository>();
            //services.AddScoped<ITokenServicesRepository, TokenServicesRepository>();


            //services.AddAutoMapper(typeof(tCartonMapper).Assembly);

            services.AddAutoMapper(typeof(tlrmCartonContext).Assembly);

            return services;
        }
    }
}
