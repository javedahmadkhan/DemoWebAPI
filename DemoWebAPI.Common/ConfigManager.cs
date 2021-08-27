using Demo.Common.Contstants;
using Microsoft.Extensions.Configuration;

namespace Demo.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager
    {
        private readonly IConfiguration configuration;

        public ConfigManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            string conStr = configuration.GetSection($"ConnectionString:{Constants.conStr}").Value;
            return conStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAppInsightsConnectionString()
        {
            string conStr = configuration.GetSection($"ApplicationInsights:{Constants.appInsightsKey}").Value;
            return conStr;
        }
    }
}