//
// Copyright:   Copyright (c) 
//
// Description: Logger Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//

using log4net;
using Demo.Common.Enums;
using System.Threading;

namespace Demo.WebAPI
{
    /// <summary>
    /// This class is used for logging
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log Messge Method
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="logType">Log Type</param>
        public static void LogMsg(string msg, Enums.LogType logType)
        {
            ILog log = LogManager.GetLogger(typeof(Logger));

            switch (logType)
            {
                case Enums.LogType.DEBUG:
                    ThreadPool.QueueUserWorkItem(task => log.Debug(msg));
                    break;

                case Enums.LogType.INFO:
                    ThreadPool.QueueUserWorkItem(task => log.Info(msg));
                    break;

                case Enums.LogType.ERROR:
                    ThreadPool.QueueUserWorkItem(task => log.Error(msg));
                    break;
                default:
                    ThreadPool.QueueUserWorkItem(task => log.Info(msg));
                    break;
            }
        }
    }
}