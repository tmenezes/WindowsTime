using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using WindowsTime.Monitorador.Api.Extensions;
using WindowsTime.Monitorador.Api.Helpers;
using WindowsTime.Monitorador.Api.Structs;

namespace WindowsTime.Monitorador.Api
{
    public static class WindowsApi
    {
        private const uint APPMODEL_ERROR_NO_PACKAGE = 15700;
        private const uint ERROR_SUCCESS = 0;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
        private const uint SECURITY_MANDATORY_HIGH_RID = 0x00003000;
        private const uint TOKEN_READ = 0x00020008;

        private static IDictionary<int, IntPtr> windowsStoreWindowsHandles = new ConcurrentDictionary<int, IntPtr>(); // process Id, handle janela principal

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        [DllImport("user32.dll")]
        static extern bool IsImmersiveProcess(IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int GetPackageId(IntPtr hProcess, ref int bufferLength, IntPtr pBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPackageFullName(IntPtr hProcess, ref uint packageFullNameLength, StringBuilder packageFullName);

        [DllImport("user32.dll", EntryPoint = "GetPropW", CharSet = CharSet.Unicode)]
        private static extern int GetProp(IntPtr hwnd, string lpString);

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool EnumProcesses(int[] lpidProcess, int cb, out int cbNeeded);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);


        [HandleProcessCorruptedStateExceptions()]
        public static int GetActiveWindowHandle()
        {
            return GetForegroundWindow();
        }

        [HandleProcessCorruptedStateExceptions()]
        public static string GetWindowsText(int handle)
        {
            const int chars = 256;
            var buffer = new StringBuilder(chars);

            return GetWindowText(handle, buffer, chars) > 0
                       ? buffer.ToString()
                       : "[ indefinido ]";
        }

        [HandleProcessCorruptedStateExceptions()]
        public static Process GetProcess(int handle)
        {
            uint processId;

            return GetWindowThreadProcessId((IntPtr)handle, out processId) > 0
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
        public static WindowsStorePackageId GetWindowsStorePackageId(Process process)
        {
            int len = 0;
            int retval = GetPackageId(process.Handle, ref len, IntPtr.Zero);
            //if (retval != ERROR_INSUFFICIENT_BUFFER)
            //    throw new Win32Exception();

            IntPtr buffer = Marshal.AllocHGlobal((int)len);
            try
            {
                retval = GetPackageId(process.Handle, ref len, buffer);
                //if (retval != ERROR_SUCCESS)
                //    throw new Win32Exception();
                PACKAGE_ID packageID = (PACKAGE_ID)Marshal.PtrToStructure(buffer, typeof(PACKAGE_ID));

                var fullname = GetWindowsStorePackageFullName(process);
                return new WindowsStorePackageId(packageID, fullname);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [HandleProcessCorruptedStateExceptions()]
        public static string GetWindowsStorePackageFullName(Process process)
        {
            try
            {
                uint packageFullNameLength = 0;
                var packageFullNameBld = new StringBuilder();

                var ret = GetPackageFullName(process.Handle, ref packageFullNameLength, packageFullNameBld);

                //if ((ret == APPMODEL_ERROR_NO_PACKAGE) || (packageFullNameLength == 0))
                //{
                //    // Not a WindowsStoreApp process
                //    return;
                //}

                // Call again, now that we know the size
                packageFullNameBld = new StringBuilder((int)packageFullNameLength);
                ret = GetPackageFullName(process.Handle, ref packageFullNameLength, packageFullNameBld);

                return packageFullNameBld.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HandleProcessCorruptedStateExceptions()]
        public static IntPtr GetWindowsStoreWindowHandle(Process process)
        {
            try
            {
                if (windowsStoreWindowsHandles.ContainsKey(process.Id))
                    return windowsStoreWindowsHandles[process.Id];

                lock ("lock_GetWindowsStoreWindowHandle")
                {
                    if (windowsStoreWindowsHandles.ContainsKey(process.Id))
                        return windowsStoreWindowsHandles[process.Id];

                    LoadWindowsStoreProcess();
                }

                return windowsStoreWindowsHandles.ContainsKey(process.Id)
                    ? windowsStoreWindowsHandles[process.Id]
                    : IntPtr.Zero;
            }
            catch (Exception)
            {
                return IntPtr.Zero;
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


        private static void LoadWindowsStoreProcess()
        {
            var pids = new int[1024];
            int bytesNeeded;

            if (!EnumProcesses(pids, 1024, out bytesNeeded))
            {
                pids = Process.GetProcesses().Where(IsWindowsStoreApp).Select(p => p.Id).ToArray();
            }

            windowsStoreWindowsHandles.Clear();

            for (int i = 0; i < pids.Length; i++)
            {
                if (windowsStoreWindowsHandles.ContainsKey(pids[i]))
                    continue;

                //var processHandle = OpenProcess(/*PROCESS_ALL_ACCESS*/ 2035711, false, pids[i]); CloseHandle(processHandle);
                //var handle = FindWindowsStoreWindowHandle(pids[i], processHandle);
                var handle = FindWindowsStoreWindowHandle(Process.GetProcessById(pids[i]));
                if (handle != IntPtr.Zero)
                {
                    windowsStoreWindowsHandles.Add(pids[i], handle);
                }
            }
        }

        private static IntPtr FindWindowsStoreWindowHandle(Process process)
        {
            var isWindowsStoreApp = IsWindowsStoreApp(process); //IsImmersiveProcess(processHandle); //IsWindowsStoreApp(process);
            if (!isWindowsStoreApp)
                return IntPtr.Zero;


            bool found = false;
            var prevWindow = IntPtr.Zero;

            while (!found)
            {
                var desktopWindow = GetDesktopWindow();
                if (desktopWindow == IntPtr.Zero)
                    break;

                var nextWindow = FindWindowEx(desktopWindow, prevWindow, null, null);
                if (nextWindow == IntPtr.Zero)
                    break;

                // Check whether window belongs to the correct process.
                var nextWindowProcess = GetProcess((int)nextWindow);

                if (nextWindowProcess == null)
                    break;

                if (nextWindowProcess.Id == process.Id)
                {
                    // Add additional checks. In my case, I had to bring the window to front so these checks were necessary.
                    if (IsWindowVisible(nextWindow)
                        && !IsIconic(nextWindow)
                        && GetParent(nextWindow) == IntPtr.Zero)
                        return nextWindow;
                    else
                    {
                        var parent = GetParent(nextWindow);
                        var validParent = IsWindowVisible(parent) && !IsIconic(parent);
                        //&& GetProp(parent, "Windows.ImmersiveShell.IdentifyAsMainCoreWindow") > 0;
                        if (validParent)
                            return parent;
                    }
                }

                prevWindow = nextWindow;
            }

            return IntPtr.Zero;
        }        
    }
}