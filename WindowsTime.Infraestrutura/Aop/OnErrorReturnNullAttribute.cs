using System;
using PostSharp.Aspects;

namespace WindowsTime.Infraestrutura.Aop
{
    [Serializable]
    public class OnErrorReturnNullAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            LogAspectAttribute.Logger.ErrorException("OnErrorReturnNull: " + args.Method.Name, args.Exception);

            args.FlowBehavior = FlowBehavior.Return;
            args.ReturnValue = null;
        }
    }
}
