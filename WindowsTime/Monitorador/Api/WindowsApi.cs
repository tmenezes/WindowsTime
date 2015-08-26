using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using WindowsTime.Monitorador.Api.Extensions;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador.Api
{
    public static class WindowsApi
    {
        private const uint APPMODEL_ERROR_NO_PACKAGE = 15700;
        private const uint ERROR_SUCCESS = 0;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
        private const uint SECURITY_MANDATORY_HIGH_RID = 0x00003000;
        private const uint TOKEN_READ = 0x00020008;


        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        [DllImport("user32.dll")]
        internal static extern bool IsImmersiveProcess(IntPtr hProcess);

        [DllImport("user32.dll", EntryPoint = "GetPropW", CharSet = CharSet.Unicode)]
        internal static extern int GetProp(IntPtr hwnd, string lpString);

        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool EnumProcesses(int[] lpidProcess, int cb, out int cbNeeded);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr hObject);

        internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false, ThrowOnUnmappableChar = true)]
        private static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf, IntPtr ppvReserved);


        [HandleProcessCorruptedStateExceptions()]
        public static IntPtr GetActiveWindowHandle()
        {
            return GetForegroundWindow();
        }

        [HandleProcessCorruptedStateExceptions()]
        public static string GetWindowsText(IntPtr handle)
        {
            const int chars = 256;
            var buffer = new StringBuilder(chars);

            return GetWindowText(handle, buffer, chars) > 0
                       ? buffer.ToString()
                       : "[ indefinido ]";
        }

        [HandleProcessCorruptedStateExceptions()]
        public static Process GetProcess(IntPtr handle)
        {
            uint processId;

            return GetWindowThreadProcessId(handle, out processId) > 0
                       ? ProcessHelper.GetProcess((int)processId)
                       : null;
        }

        [HandleProcessCorruptedStateExceptions()]
        public static bool IsWindowsStoreApp(Process process)
        {
            try
            {
                return (process != null) && IsImmersiveProcess(process.Handle) && !(process.IsExplorerProcess());
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions()]
        public static List<IntPtr> GetChildWindows(IntPtr windowHanle)
        {
            var result = new List<IntPtr>();

            try
            {
                EnumChildWindows(windowHanle, (handle, pointer) =>
                {
                    result.Add(handle);
                    return true;
                }, IntPtr.Zero);
            }
            catch (Exception)
            {
            }

            return result;
        }

        [HandleProcessCorruptedStateExceptions()]
        public static string GetResourceString(string resourcePath, string resourceKey)
        {
            string resourceManifestString = string.Format("@{{{0}? {1}}}", resourcePath, resourceKey);
            var outBuff = new StringBuilder(1024);

            int result = SHLoadIndirectString(resourceManifestString, outBuff, outBuff.Capacity, IntPtr.Zero);
            return outBuff.ToString();
        }


        public static Icon GetWindowsShell32Icon(int number, bool largeIcon)
        {
            return GetIcon("shell32.dll", number, largeIcon);
        }

        public static Icon GetExplorerIcon(int number, bool largeIcon)
        {
            var explorerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "explorer.exe");
            return GetIcon(explorerPath, number, largeIcon);
        }

        [HandleProcessCorruptedStateExceptions()]
        public static Icon GetIcon(string filename, int number, bool largeIcon)
        {
            try
            {
                IntPtr large, small;
                ExtractIconEx(filename, number, out large, out small, 1);

                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }
        }
    }
}