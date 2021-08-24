using Microsoft.Extensions.Configuration;
using Demo.Common.Contstants;

namespace Demo.Common
{
    public class CommonConfig
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public CommonConfig(IConfiguration configuration)
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
