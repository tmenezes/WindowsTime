using System;

namespace WindowsTime.Infraestrutura.Framework.Logging
{
    public class Logger
    {
        private static readonly ILogger _logger = new NLogLogger();


        public static ILogger GetLogger()
        {
            return _logger;
        }

        public static void Log(string message, LogTypeEnum logType)
        {
            _logger.Log(message, logType);
        }
        public static void Debug(string message)
        {
            _logger.Debug(message);
        }
        public static void Info(string message)
        {
            _logger.Info(message);
        }
        public static void Trace(string message)
        {
            _logger.Trace(message);
        }
        public static void Error(string message)
        {
            _logger.Error(message);
        }
        public static void ErrorException(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }
    }
}
