using System;
using PostSharp.Aspects;

namespace WindowsTime.Infraestrutura.Framework.Aop
{
    [Serializable]
    public class OnErrorResumeNextAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            LogAspectAttribute.Logger.ErrorException("OnErrorResumeNext: " + args.Method.Name, args.Exception);

            args.FlowBehavior = FlowBehavior.Continue;
            args.ReturnValue = null;
        }
    }
}