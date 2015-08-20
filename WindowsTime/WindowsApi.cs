using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsTime
{
    public static class WindowsApi
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PACKAGE_ID
        {
            public uint reserved;
            public uint processorArchitecture;
            public PACKAGE_VERSION version;
            public IntPtr name;
            public IntPtr publisher;
            public IntPtr resourceId;
            public IntPtr publisherId;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PACKAGE_VERSION
        {
            [FieldOffset(0)]
            public UInt64 Version;
            [FieldOffset(0)]
            public ushort Revision;
            [FieldOffset(2)]
            public ushort Build;
            [FieldOffset(4)]
            public ushort Minor;
            [FieldOffset(6)]
            public ushort Major;
        }

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int GetPackageId(IntPtr hProcess, ref int bufferLength, IntPtr pBuffer);


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
                       : "[ indefinido ]";
        }

        public static Process GetProcess(int handle)
        {
            uint processId;

            return GetWindowThreadProcessId((IntPtr)handle, out processId) > 0
                       ? ProcessHelper.GetProcess((int)processId)
                       : null;
        }

        public static PACKAGE_ID GetPackageId(int handle)
        {
            var hprocess = ProcessHelper.GetProcess(handle).Handle;
            int len = 0;
            int retval = GetPackageId(hprocess, ref len, IntPtr.Zero);
            //if (retval != ERROR_INSUFFICIENT_BUFFER)
            //    throw new Win32Exception();

            IntPtr buffer = Marshal.AllocHGlobal((int)len);
            try
            {
                retval = GetPackageId(hprocess, ref len, buffer);
                //if (retval != ERROR_SUCCESS)
                //    throw new Win32Exception();
                PACKAGE_ID packageID = (PACKAGE_ID)Marshal.PtrToStructure(buffer, typeof(PACKAGE_ID));

                return packageID;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
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