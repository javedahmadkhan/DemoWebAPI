using Demo.Common.Contstants;
using System;

namespace Demo.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string conStr = Environment.GetEnvironmentVariable(Constants.conStr);
            return conStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetAppInsightsConnectionString()
        {
            string conStr = Environment.GetEnvironmentVariable(Constants.appInsightsKey);
            return conStr;
        }
    }
}
