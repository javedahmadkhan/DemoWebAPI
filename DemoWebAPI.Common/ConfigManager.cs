using Microsoft.Extensions.Configuration;
using Demo.Common.Contstants;

namespace Demo.Common
{
    public class ConfigManager
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public ConfigManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetConnectionString()
        {
            string conStr = Configuration[$"ConnectionString:{Constants.conStr}"];
            return conStr;
        }

        public string GetAppInsightsConnectionString()
        {

            string conStr = Configuration[$"ConnectionString:{Constants.appInsightsKey}"];
            return conStr;
        }

    }
}
