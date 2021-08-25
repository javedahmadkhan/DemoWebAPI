using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.WebAPI.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "memory_check";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var options = _options.Get(context.Registration.Name);

            // Include GC information in the reported diagnostics.
            var allocated = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
            {
                { "AllocatedBytes", allocated },
                { "Gen0Collections", GC.CollectionCount(0) },
                { "Gen1Collections", GC.CollectionCount(1) },
                { "Gen2Collections", GC.CollectionCount(2) },
             };

            var status = (allocated < options.Threshold) ?
                HealthStatus.Healthy : context.Registration.FailureStatus;

            return Task.FromResult(new HealthCheckResult(
                status,
                description: "Reports degraded status if allocated bytes " +
                    $">= {options.Threshold} bytes.",
                exception: null,
                data: data));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MemoryCheckOptions
    {
        // Failure threshold (in bytes)
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }
}
