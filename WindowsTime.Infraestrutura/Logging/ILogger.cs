using System;

namespace WindowsTime.Infraestrutura.Logging
{
    public interface ILogger
    {
        void Log(string message, LogTypeEnum logType);
        void Debug(string message);
        void Info(string message);
        void Trace(string message);
        void Error(string message);
        void ErrorException(string message, Exception ex);
    }
}