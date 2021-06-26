using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.Api.Middleware;

using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddApplicationServices(Configuration);
            services.AddCors();
            services.AddIdentityServices(Configuration);


            //services.AddAuthentication()
            //   .AddJwtBearer(options =>
            //   {
            //       options.RequireHttpsMetadata = false;
            //       options.SaveToken = true;

            //       options.TokenValidationParameters = new TokenValidationParameters()
            //       {
            //           ValidIssuer = authorizationConfiguration["Issuer"],
            //           ValidAudience = authorizationConfiguration["Issuer"],
            //           IssuerSigningKey =
            //               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfiguration["Key"]))
            //       };
            //   });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transnational Lanka Carton API Documentation", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });        

            });           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {              
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tlrsCartonManager.Api v1"));
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(XmlConfigurationExtensions => XmlConfigurationExtensions.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseAuthentication();           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
