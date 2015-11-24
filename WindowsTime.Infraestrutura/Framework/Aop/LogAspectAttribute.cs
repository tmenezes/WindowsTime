using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PostSharp.Aspects;
using WindowsTime.Infraestrutura.Framework.Logging;

namespace WindowsTime.Infraestrutura.Framework.Aop
{
    [Serializable]
    public class LogAspectAttribute : OnMethodBoundaryAspect
    {
        // atributos
        private static readonly ILogger _logger = Logging.Logger.GetLogger();
        private List<Type> _collections = null;

        // propriedades
        public static ILogger Logger
        {
            get { return _logger; }
        }
        public LogTypeEnum LogType { get; set; }

        // construtores
        public LogAspectAttribute()
            : this(LogTypeEnum.Trace)
        {
        }

        public LogAspectAttribute(LogTypeEnum logType)
        {
            this.LogType = logType;
        }


        // publicos
        public override void OnEntry(MethodExecutionArgs args)
        {
            string startMethodText = string.Format("Inicío Método : {0} ({1})", args.Method.Name, args.Method.ReflectedType.FullName);
            Log(startMethodText);

            if (args.Arguments.Count > 0)
            {
                string argumentsDetail = Enumerable.Select<object, string>(args.Arguments, a => a != null ? a.ToString() : "null")
                                                       .Aggregate((um, outro) => string.Format("{0}, {1}", um, outro));

                Log(string.Format("Arguments\t\t: {0}", argumentsDetail));
            }
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            string returnValue = string.Empty;

            if (args.ReturnValue != null)
            {
                returnValue = args.ReturnValue.ToString();

                if (IsCollection(args.ReturnValue.GetType()))
                {
                    var quantidadeDeElementos = ((IEnumerable)args.ReturnValue).OfType<object>().Count();
                    Log(string.Format("Return\t\t: IEnumerable, [{0}] Elemento(s)", quantidadeDeElementos));
                }
                else
                {
                    Log(string.Format("Return\t\t: {0}", returnValue));
                }
            }
            else
            {
                returnValue = "void / null";
                Log(string.Format("Return\t\t: {0}", returnValue));
            }
        }
        public override void OnException(MethodExecutionArgs args)
        {
            _logger.ErrorException("Exceção\t\t:", args.Exception);
        }
        public override void OnExit(MethodExecutionArgs args)
        {
            string endMethodText = string.Format((string)"Fim Método\t: {0}", (object)args.Method.Name);
            Log(endMethodText);
        }


        // privados
        private void Log(string message)
        {
            _logger.Log(message, this.LogType);
        }
        private bool IsCollection(Type type)
        {
            if (_collections == null)
                _collections = new List<Type>() { typeof(IEnumerable<>), typeof(IEnumerable) };

            bool isString = type == typeof(string);
            bool isCollection = type.GetInterfaces().Any(i => this._collections.Any(c => i == c));

            return !isString && isCollection;
        }
    }
}
