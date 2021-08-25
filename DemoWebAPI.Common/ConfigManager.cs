using Microsoft.Extensions.Configuration;
using Demo.Common.Contstants;

namespace Demo.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            string conStr = Configuration[$"ConnectionString:{Constants.conStr}"];
            return conStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAppInsightsConnectionString()
        {

            string conStr = Configuration[$"ApplicationInsights:{Constants.appInsightsKey}"];
            return conStr;
        }
    }
}
