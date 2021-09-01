//
// Copyright:   Copyright (c) 
//
// Description: Config Manager Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Common.Contstants;
using Microsoft.Extensions.Configuration;

namespace Demo.Common
{
    /// <summary>
    /// This class is used for managing config details
    /// </summary>
    public class ConfigManager
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor for Config Manager
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public ConfigManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Get Connection String Method
        /// </summary>
        /// <returns>Connection String</returns>
        public string GetConnectionString()
        {
            string conStr = configuration.GetSection($"ConnectionString:{Constants.conStr}").Value;
            return conStr;
        }

        /// <summary>
        /// Get AppInsights Connection String Method
        /// </summary>
        /// <returns>Connection String</returns>
        public string GetAppInsightsConnectionString()
        {
            string conStr = configuration.GetSection($"ApplicationInsights:{Constants.appInsightsKey}").Value;
            return conStr;
        }
    }
}