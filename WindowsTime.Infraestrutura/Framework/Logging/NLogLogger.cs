using System;
using NLog;

namespace WindowsTime.Infraestrutura.Framework.Logging
{
    internal class NLogLogger : ILogger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();


        public void Log(string message, LogTypeEnum logType)
        {
            switch (logType)
            {
                case LogTypeEnum.Debug:
                    Debug(message);
                    break;

                case LogTypeEnum.Info:
                    Info(message);
                    break;

                case LogTypeEnum.Trace:
                    Trace(message);
                    break;

                case LogTypeEnum.Error:
                    Error(message);
                    break;
            }
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void ErrorException(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }
    }
}