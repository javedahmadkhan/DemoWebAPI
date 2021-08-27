using log4net;
using Demo.Common.Enums;
using System.Threading;

namespace Demo.WebAPI
{
    public static class Logger
    {
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

