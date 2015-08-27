using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace WindowsTime.Monitorador.Helpers
{
    public static class MarshalHelper
    {
        [HandleProcessCorruptedStateExceptions()]
        public static string SafePtrToStringUni(IntPtr intPtr)
        {
            try
            {
                return Marshal.PtrToStringUni(intPtr);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
