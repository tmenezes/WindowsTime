using System;
using PostSharp.Aspects;

namespace WindowsTime.Infraestrutura.Aop
{
    [Serializable]
    public class RetryAspectAttribute : MethodInterceptionAspect
    {
        // propriedades
        public int MaxRetries { get; set; }
        public Type ExceptionType { get; set; }


        // construtor
        public RetryAspectAttribute()
        {
            MaxRetries = 3;
            ExceptionType = typeof(Exception);
        }


        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var tentativas = 0;
            while (tentativas < MaxRetries)
            {
                try
                {
                    args.Proceed();
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == ExceptionType)
                    {
                        tentativas++;
                        LogAspectAttribute.Logger.ErrorException(string.Format("RetryAspectAttribute(Try: {0}): {1}", tentativas, args.Method.Name), ex);
                        continue;
                    }

                    throw;
                }
            }
        }
    }
}