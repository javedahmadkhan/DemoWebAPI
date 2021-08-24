using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Contstants
{
    public class Constants
    {
        public const string conStr = "DemoWebAPI";

        public const string appInsightsKey = "APPINSIGHTS_CONNECTIONSTRING";

        public const string ResourceId = "https://storage.azure.com/";
        
        public const string AuthInstance = "https://login.microsoftonline.com/{0}/";

        public const string azurePathInBlob = "dev/files/myfile.txt";

        public const string logPath = "Logs/mylog-{Date}.txt";

        public const string healthCheckUIpath= "/health-ui";

        public const string healthCheckAPIpath = "/health-api";

        public const string approle1 = "web-api-with-roles-user";

        public const string approle2 = "web-api-with-roles-user2";

        public const string approleAdmin = "web-api-with-roles-admin";
    }
}
