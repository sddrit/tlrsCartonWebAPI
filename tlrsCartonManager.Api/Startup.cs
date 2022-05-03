using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.Api.Middleware;
using tlrsCartonManager.Api.Util.Options;

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
            var authorizationConfiguration = Configuration.GetSection("Token");
            services.Configure<TokenConfiguration>(Configuration.GetSection("Token"));

            services.AddHttpContextAccessor();

            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                       
                        ValidIssuer = authorizationConfiguration["Issuer"],
                        ValidAudience = authorizationConfiguration["Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfiguration["Key"]))
                    };
                });
            services.AddApplicationServices(Configuration);


            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Transnational Lanka RMS API Documentation",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Sajith Lakjaya",
                        Email = "slakjaya@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });

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
            if (env.IsEnvironment("DevelopmentLk") || env.IsEnvironment("DevelopmentId") || env.IsEnvironment("UatId") || env.IsEnvironment("UatLk"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tlrsCartonManager.Api v1 " + env.EnvironmentName));
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
