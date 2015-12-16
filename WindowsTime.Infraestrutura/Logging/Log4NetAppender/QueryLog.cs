using System;
using log4net.Core;

namespace WindowsTime.Infraestrutura.Logging.Log4NetAppender
{
    public class QueryLog
    {
        // fields
        private DateTime _startLoadingObjects;

        // properties
        public DateTime ExecutionDate { get; set; }
        public string Query { get; set; }
        public int RowsProcessed { get; set; }

        public double QueryExecutionTime { get; set; }
        public double LoadObjectsTime { get; set; }
        public double TotalExecutionTime { get; set; }

        public bool IsFinalized { get; set; }


        // constructor
        public QueryLog(LoggingEvent loggingEvent)
        {
            ProcessLoggingEvent(loggingEvent);
        }


        // publics
        public QueryLog ProcessLoggingEvent(LoggingEvent loggingEvent)
        {
            if (!loggingEvent.IsLoaderLogEvent())
                return this;


            if (loggingEvent.IsSelectLogEvent())
            {
                LoadQuery(loggingEvent);
                return this;
            }

            if (loggingEvent.IsBeginResultSetLogEvent())
            {
                PrepareToLoadObjects(loggingEvent);
                return this;
            }

            if (loggingEvent.IsResultRowLogEvent())
            {
                LoadObject();
                return this;
            }

            if (loggingEvent.IsEndResultSetLogEvent())
            {
                FinalizeQueryLog(loggingEvent);
                return this;
            }

            return this;
        }

        public override string ToString()
        {
            return $"RowsProcessed: {RowsProcessed}, QueryExecutionTime: {QueryExecutionTime}, LoadObjectsTime: {LoadObjectsTime}, TotalExecutionTime: {TotalExecutionTime}, Query: {Query}";
        }


        // private
        private void LoadQuery(LoggingEvent loggingEvent)
        {
            ExecutionDate = loggingEvent.TimeStamp;
            Query = loggingEvent.RenderedMessage;
        }

        private void PrepareToLoadObjects(LoggingEvent loggingEvent)
        {
            QueryExecutionTime = (loggingEvent.TimeStamp - ExecutionDate).TotalSeconds;
            _startLoadingObjects = loggingEvent.TimeStamp;
        }

        private void LoadObject()
        {
            RowsProcessed++;
        }

        private void FinalizeQueryLog(LoggingEvent loggingEvent)
        {
            LoadObjectsTime = (loggingEvent.TimeStamp - _startLoadingObjects).TotalSeconds;
            TotalExecutionTime = (loggingEvent.TimeStamp - ExecutionDate).TotalSeconds;
            IsFinalized = true;
        }
    }
}