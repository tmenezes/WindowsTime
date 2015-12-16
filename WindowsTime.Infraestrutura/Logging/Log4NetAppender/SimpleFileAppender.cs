using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using log4net.Appender;
using log4net.Core;

namespace WindowsTime.Infraestrutura.Logging.Log4NetAppender
{
    public class SimpleFileAppender : AppenderSkeleton
    {
        // atributos
        private const string ARQUIVO_LOG = "log4netLog.txt";
        private static readonly ConcurrentDictionary<string, QueryLog> _queryLogs = new ConcurrentDictionary<string, QueryLog>();

        // props
        protected override bool RequiresLayout => true;
        protected static string CaminhoDoArquivoDeLog => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ARQUIVO_LOG);

        // construtor
        static SimpleFileAppender()
        {
            try
            {
                File.Delete(CaminhoDoArquivoDeLog);
            }
            catch (Exception) { }
        }


        protected override void Append(LoggingEvent loggingEvent)
        {
            //string title = $"{loggingEvent.Level.DisplayName} {loggingEvent.LoggerName}";

            var message = RenderLoggingEvent(loggingEvent);

            SaveFileContent(message);

            var queryLog = _queryLogs.AddOrUpdate(loggingEvent.ThreadName,
                                                  key => new QueryLog(loggingEvent),
                                                  (key, log) => log.ProcessLoggingEvent(loggingEvent));
            if (queryLog.IsFinalized)
            {
                SaveFileContent(queryLog.ToString() + Environment.NewLine);

                _queryLogs.TryRemove(loggingEvent.ThreadName, out queryLog);
            }
        }

        private static void SaveFileContent(string content)
        {
            using (var writer = new StreamWriter(CaminhoDoArquivoDeLog, true, Encoding.Default))
            {
                writer.Write(content);
                writer.Flush();
            }
        }
    }
}
