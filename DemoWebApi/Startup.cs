using AutoMapper;
using Demo.BusinessLogic.AutoMapperProfile;
using Demo.Common;
using Demo.Entities.DataContext;
using Demo.WebAPI.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using Demo.Common.Contstants;

namespace DemoWebApi
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
            var con = new CommonConfig(Configuration);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

            services.Configure<OpenIdConnectOptions>(
               OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   options.TokenValidationParameters.RoleClaimType = "roles";
                   options.TokenValidationParameters.NameClaimType = "name";
               });
            services.AddAuthorization(policies =>
            {
                policies.AddPolicy("p-web-api-with-roles-user", p =>
                {
                    p.RequireClaim("roles", Demo.Common.Contstants.Constants.approle1);
                });
                policies.AddPolicy("p-web-api-with-roles-user2", p =>
                {
                    p.RequireClaim("roles", Demo.Common.Contstants.Constants.approle2);
                });
                policies.AddPolicy("p-web-api-with-roles-admin", p =>
                {
                    p.RequireClaim("roles", Demo.Common.Contstants.Constants.approleAdmin);
                });

                policies.AddPolicy("ValidateAccessTokenPolicy", validateAccessTokenPolicy =>
                {
                    validateAccessTokenPolicy.RequireClaim("scp", "access_as_user");

                    // Validate id of application for which the token was created
                    // The id of UI application
                    validateAccessTokenPolicy.RequireClaim("azp", "id of UI application");

                    // Indicates how the client was authenticated. For a public client, the value is "0". 
                    // If client ID and client secret are used, the value is "1". 
                    // If a client certificate was used for authentication, the value is "2".
                    validateAccessTokenPolicy.RequireClaim("azpacr", "1");
                });
            });

            //services.AddControllers(options =>
            //{
            //    // global
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddControllers();                 

            services.AddDbContext<DemoDBContext>(options =>
            {
                options.UseSqlServer(con.GetConnectionString(),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.ConfigureCors();
            services.ConfigureUnitOfWork();
            services.ConfigureBusinessService();
            // services.ConfigureHTTPClientFactory();
            services.ConfigureHealthChecks(Configuration);
            services.ConfigureHealthChecksUI();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoWebApi", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
            services.AddSingleton(mapperConfig.CreateMapper());
            
            services.AddApplicationInsightsTelemetry(con.GetAppInsightsConnectionString());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DemoDBContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            DBInitializer.Initialize(context);
            loggerFactory.AddFile(Demo.Common.Contstants.Constants.logPath);

            app.UseEndpoints(endpoints =>
            {
                // HealthCheck middleware
                endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI(setup =>
                {
                    setup.UIPath = Demo.Common.Contstants.Constants.healthCheckUIpath;
                    setup.ApiPath = Demo.Common.Contstants.Constants.healthCheckAPIpath;
                });

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
