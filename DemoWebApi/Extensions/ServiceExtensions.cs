using Demo.BusinessLogic.Contract;
using Demo.BusinessLogic.Service;
using Demo.Repository.UnitOfWork.Contract;
using Demo.Repository.UnitOfWork.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;

namespace Demo.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            var sqlConnectionString = config["ConnectionString:DemoWebAPI"];
            var accountName = config.GetValue<string>("AzureStorageAccountName");
            var accountKey = config.GetValue<string>("AzureStorageAccountKey");

            // Registers required services for health checks        
            var hcBuilder = services.AddHealthChecks();

            // Add a health check
            hcBuilder
                .AddSqlServer(
                    sqlConnectionString,
                    name: "DB health check",
                    tags: new string[] { "demodb" });

            if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            {
                hcBuilder
                    .AddAzureBlobStorage(
                        $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
                        name: "demo-storage-check",
                        tags: new string[] { "demostorage" });
            }
        }

        public static void ConfigureHealthChecksUI(this IServiceCollection services)
        {
            services.AddHealthChecksUI(opt =>
           {
               opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
               opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
               opt.SetApiMaxActiveRequests(1); //api requests concurrency    
               opt.AddHealthCheckEndpoint("default api", "/api/health"); //map health check api    
           })
          .AddInMemoryStorage();
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureBusinessService(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemManagementService, TodoItemManagementService>();
        }

        public static void ConfigureHTTPClientFactory(this IServiceCollection services)
        {
            services.AddHttpClient<ITodoItemManagementService, TodoItemManagementService>(client =>
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(delay);
        }

        #region Circuit Breaker Policy
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .AdvancedCircuitBreakerAsync(0.25, TimeSpan.FromSeconds(60), 7, TimeSpan.FromSeconds(30), OnBreak, OnReset, OnHalfOpen);
        }

        private static void OnHalfOpen()
        {
            Console.WriteLine("Circuit in test mode, one request will be allowed.");
        }

        private static void OnReset()
        {
            Console.WriteLine("Circuit closed, requests flow normally.");
        }

        private static void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan span)
        {
            Console.WriteLine("Circuit cut, requests will not flow.");
        }
        #endregion

        #region

        #endregion
    }
}
