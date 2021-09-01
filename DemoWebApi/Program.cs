//
// Copyright:   Copyright (c) 
//
// Description: Program Class
//
// Project: 
//
// Author:  Javed Ahmad Khan
//
// Created Date:  
//
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace DemoWebApi
{
    /// <summary>
    /// This Class is used for configuring startup class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args">Array</param>
        public static void Main(string[] args)
        {
            var log4netRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Host Builder Method
        /// </summary>
        /// <param name="args">Array</param>
        /// <returns>Host Builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging(logBuilder =>
            {
                logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                logBuilder.AddConsole();
                logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
