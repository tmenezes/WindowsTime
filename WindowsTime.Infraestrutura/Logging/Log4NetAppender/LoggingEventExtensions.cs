using log4net.Core;

namespace WindowsTime.Infraestrutura.Logging.Log4NetAppender
{
    public static class LoggingEventExtensions
    {
        private const string LOADER_LOG_EVENT = "NHibernate.Loader.Loader";
        private const string SELECT_LOG_EVENT = "select";
        private const string BEGIN_RESULT_SET_LOG_EVENT = "processing result set";
        private const string ROW_LOG_EVENT = "result set row";
        private const string END_RESULT_SET_LOG_EVENT = "done processing result set";


        public static bool IsLoaderLogEvent(this LoggingEvent loggingEvent)
        {
            return loggingEvent.LoggerName == LOADER_LOG_EVENT;
        }

        public static bool IsSelectLogEvent(this LoggingEvent loggingEvent)
        {
            return loggingEvent.RenderedMessage.ToLower().StartsWith(SELECT_LOG_EVENT);
        }

        public static bool IsBeginResultSetLogEvent(this LoggingEvent loggingEvent)
        {
            return loggingEvent.RenderedMessage.ToLower().StartsWith(BEGIN_RESULT_SET_LOG_EVENT);
        }

        public static bool IsResultRowLogEvent(this LoggingEvent loggingEvent)
        {
            return loggingEvent.RenderedMessage.ToLower().StartsWith(ROW_LOG_EVENT);
        }

        public static bool IsEndResultSetLogEvent(this LoggingEvent loggingEvent)
        {
            return loggingEvent.RenderedMessage.ToLower().StartsWith(END_RESULT_SET_LOG_EVENT);
        }
    }
}