using Demo.BusinessLogic.Contract;
using Demo.BusinessLogic.Service;
using Demo.Common.HTTPClientFactory.Contract;
using Demo.Common.HTTPClientFactory.Service;
using Demo.Repository.UnitOfWork.Contract;
using Demo.Repository.UnitOfWork.Service;
using Demo.WebAPI.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Serilog;
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
            var accountName = config.GetValue<string>("AzureStorageAccountName");
            var accountKey = config.GetValue<string>("AzureStorageAccountKey");

            // Registers required services for health checks        
            var hcBuilder = services.AddHealthChecks();

            // Add a health check
            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(
                    config["ConnectionString:DemoWebAPI"],
                    name: "Database health check",
                    tags: new string[] { "SQLDatabase" });

            if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            {
                hcBuilder
                    .AddAzureBlobStorage(
                        $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
                        name: "Azure Blob Storage health check",
                        tags: new string[] { "AzureBlobStorage" });
            }

            hcBuilder.AddCheck<MemoryHealthCheck>("Memory");
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
            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            services.AddHttpClient<IHttpClientService, HttpClientFactoryService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(request => request.Method == HttpMethod.Get ? GetRetryPolicy() : noOpPolicy)
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
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: OnBreak,
                    onReset: OnReset,
                    onHalfOpen: OnHalfOpen);
        }

        private static IAsyncPolicy<HttpResponseMessage> GetAdvancedCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .AdvancedCircuitBreakerAsync(
                failureThreshold: 0.25,
                samplingDuration: TimeSpan.FromSeconds(60),
                minimumThroughput: 7,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: OnBreak,
                onReset: OnReset,
                onHalfOpen: OnHalfOpen);
        }

        //The action to call when the circuit transitions to state, ready to try action executions again
        private static void OnHalfOpen()
        {
            Log.Information("Circuit in test mode, one request will be allowed.");
        }

        //The action to call when the circuit resets to a state.
        private static void OnReset()
        {
            Console.WriteLine("Circuit closed, requests flow normally.");
        }

        //The action to call when the circuit transitions to an state.
        private static void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan span)
        {
            Console.WriteLine("Circuit cut, requests will not flow.");
        }
        #endregion
    }
}
