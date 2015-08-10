using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsTime
{
    public static class WindowsApi
    {
        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        public static int GetActiveWindowHandle()
        {
            return GetForegroundWindow();
        }

        public static string GetWindowsText(int handle)
        {
            const int chars = 256;
            var buffer = new StringBuilder(chars);

            return GetWindowText(handle, buffer, chars) > 0
                       ? buffer.ToString()
                       : "{indefinido}";
        }

        public static Process GetProcess(int handle)
        {
            uint processId;

            return GetWindowThreadProcessId((IntPtr)handle, out processId) > 0
                       ? ProcessHelper.GetProcess((int)processId)
                       : null;
        }

        public static Process GetProcessByName(string processName)
        {
            return ProcessHelper.GetProcess(processName);
        }

        public static string GetWindowExecutableName(Process process)
        {
            return ProcessHelper.GetWindowExecutableName(process);
        }

        public static Icon GetProcessIcon(Process process)
        {
            return ProcessHelper.GetProcessIcon(process.Id);
        }
    }
}