using Microsoft.Extensions.Configuration;

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
            string conStr = Configuration["ConnectionString:DemoWebAPI"];
            return conStr;
        }
    }
}
