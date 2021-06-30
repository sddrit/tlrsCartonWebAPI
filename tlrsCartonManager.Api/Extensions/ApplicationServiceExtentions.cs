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
using tlrsCartonManager.Services.Report;
using tlrsCartonManager.Services.User;
using tlrsCartonManager.Services.ImportData;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

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
            services.AddScoped<ICartonStorageManagerRepository, CartonStorageManagerRepository>();
            services.AddScoped<ISearchManagerRepository, SearchManagerRepository >();
            services.AddScoped<IRequestManagerRepository, RequestManagerRepository>();
            services.AddScoped<IInvoiceManagerRepository, InvoiceManagerRepository>();
            services.AddScoped<IPickListManagerRepository ,PickListManagerRepository>();
            services.AddScoped<IInquiryManagerRepository, InquiryManagerRepository>();
            services.AddScoped<IReceiveTypeManagerRepository, ReceiveTypeManagerRepository>();
            services.AddScoped<IDisposalTimeFrameManagerRepository, DisposalTimeFrameManagerRepository>();
            services.AddScoped<IWorkOrderTypeManagerRepository, WorkOrderTypeManagerRepository>();
            services.AddScoped<IOwnershipManagerRepository, OwnershipManagerRepository>();
            services.AddScoped<IMobileDeviceManagerRepository, MobileDeviceManagerRepository>();
            services.AddScoped<IPostingTypeManagerRepository, PostingTypeManagerRepository>();
            services.AddScoped<ICompanyManagerRepository, CompanyManagerRepository>();
            services.AddScoped<ITaxTypeManagerRepository, TaxTypeManagerRepository>();
            services.AddScoped<IReportManagerRepository, ReportManagerRepository>();
            services.AddScoped<IRequestTypeManagerRepository, RequestTypeManagerRepository>();
            services.AddScoped<ILocationManagerRepository, LocationManagerRepository>();
            services.AddScoped<IGenericReportManagerRepository, GenericReportManagerRepository>();
            services.AddScoped<ReportGeneratingService>();
            services.AddScoped<IRolePermissionManagerRepository, RolePermissionManagerRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<IDocketPrintManagerRepository, DocketPrintManagerRepository>();
            services.AddScoped<IImportDataManagerRepository, ImportDataManagerRepository>();
            services.AddScoped<ImportDataService>();
            services.AddScoped<IAccountManagerRepository, AccountManagerRepository>();

            //---Meta Data registration

            services.AddScoped<BaseMetaRepositoryValidator>();

            services.AddScoped(typeof(IMetadataRepository<StorageType, StorageTypeDto>), typeof(BaseMetadataRepository<StorageType, StorageTypeDto>));
            services.AddScoped(typeof(IMetadataRepository<BillingCycle, BillingCycleDto>), typeof(BaseMetadataRepository<BillingCycle, BillingCycleDto>));
            services.AddScoped(typeof(IMetadataRepository<Route, RouteDto>), typeof(BaseMetadataRepository<Route, RouteDto>));
            services.AddScoped(typeof(IMetadataRepository<ServiceCategory, ServiceCategoryDto>), typeof(BaseMetadataRepository<ServiceCategory, ServiceCategoryDto>));
            services.AddScoped(typeof(IMetadataRepository<Department, DepartmentDto>), typeof(BaseMetadataRepository<Department, DepartmentDto>));

            //---------------------------------------------------------------

            services.AddAutoMapper(typeof(tlrmCartonContext).Assembly);
            return services;
        }
    }
}
