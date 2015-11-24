using System;
using WindowsTime.Infraestrutura.Logging;
using PostSharp.Aspects;

namespace WindowsTime.Infraestrutura.Aop
{
    [Serializable]
    public class PerformanceLoggerAttribute : OnMethodBoundaryAspect
    {
        // atributos
        private static readonly ILogger _logger = Logger.GetLogger();
        private DateTime _entryDate;

        // propriedades
        public LogTypeEnum LogType { get; set; }

        // construtores
        public PerformanceLoggerAttribute()
            : this(LogTypeEnum.Debug)
        {
        }

        public PerformanceLoggerAttribute(LogTypeEnum logType)
        {
            this.LogType = logType;
            this.AspectPriority = 5;
        }


        // publicos
        public override void OnEntry(MethodExecutionArgs args)
        {
            _entryDate = DateTime.Now;
        }
        public override void OnExit(MethodExecutionArgs args)
        {
            var exitDate = DateTime.Now;
            var totalSeconds = exitDate.Subtract(_entryDate).TotalSeconds;

            string performance = string.Format("Performance = [ Tempo total: {0}s,  Metodo: {1} ({2}), Inicio execucao: {3}, Fim execucao: {4}]",
                                               totalSeconds, args.Method.Name, args.Method.ReflectedType.FullName, _entryDate, exitDate);

            _logger.Log(performance, this.LogType);
        }
    }
}