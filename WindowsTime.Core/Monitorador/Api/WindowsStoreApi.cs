using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using WindowsTime.Core.Monitorador.Api.Structs;

namespace WindowsTime.Core.Monitorador.Api
{
    public static class WindowsStoreApi
    {
        private static readonly IDictionary<IntPtr, Process> windowsStoreWindowsHandles; // handle janela, processo
        private static readonly IDictionary<string, bool> appFrameHostNames;
        private static bool loadingStoreProcess = false;

        static WindowsStoreApi()
        {
            windowsStoreWindowsHandles = new ConcurrentDictionary<IntPtr, Process>();
            appFrameHostNames = new Dictionary<string, bool>()
                                {
                                    { "ApplicationFrameHost", true },
                                    { "WWAHost", true }
                                };
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetPackageId(IntPtr hProcess, ref int bufferLength, IntPtr pBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPackageFullName(IntPtr hProcess, ref uint packageFullNameLength, StringBuilder packageFullName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPackagePath(ref PACKAGE_ID package, uint reserved, ref uint pathLength, StringBuilder path);

        [HandleProcessCorruptedStateExceptions]
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
                var path = GetWindowsStorePackagePath(packageID);
                return new WindowsStorePackageId(packageID, fullname, path);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [HandleProcessCorruptedStateExceptions]
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
                return String.Empty;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public static string GetWindowsStorePackagePath(PACKAGE_ID packageId)
        {
            try
            {
                uint pathLength = 0;
                var pathSb = new StringBuilder();

                var ret = GetPackagePath(ref packageId, 0, ref pathLength, pathSb);

                //if ((ret == APPMODEL_ERROR_NO_PACKAGE) || (packageFullNameLength == 0))
                //{
                //    // Not a WindowsStoreApp process
                //    return;
                //}

                // Call again, now that we know the size
                pathSb = new StringBuilder((int)pathLength);
                ret = GetPackagePath(ref packageId, 0, ref pathLength, pathSb);

                return pathSb.ToString();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public static Process GetWindowsStoreRealProcess(IntPtr windowHandle)
        {
            try
            {
                var childWindows = GetChildWindows(windowHandle);
                if (!childWindows.Any())
                    return WindowsApi.GetProcess(windowHandle);


                var processes = childWindows.Select(i => WindowsApi.GetProcess(i))
                                            .GroupBy(p => p.Id)
                                            .Select(group => group.First())
                                            .Where(p => p.ProcessName != "ApplicationFrameHost")
                                            .ToList();
                if (processes.Count == 1)
                    return processes.First();

                foreach (var process in processes)
                {
                    var handle = FindWindowsStoreWindowHandle(process);
                    var isTheWindowHandleProcess = (handle == windowHandle) || (WindowsApi.GetParent(handle) == windowHandle);

                    if (isTheWindowHandleProcess)
                        return process;
                }

                return WindowsApi.GetProcess(windowHandle);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsFrameHostProcess(Process process)
        {
            return process != null && appFrameHostNames.ContainsKey(process.ProcessName);
        }


        internal static void LoadWindowsStoreProcess()
        {
            var processes = Process.GetProcesses().Where(WindowsApi.IsWindowsStoreApp).ToList();
            loadingStoreProcess = true;

            windowsStoreWindowsHandles.Clear();
            LoadWindowsStoreProcess(processes);

            loadingStoreProcess = false;
        }


        private static void LoadWindowsStoreProcess(List<Process> processes)
        {
            foreach (var process in processes)
            {
                try
                {
                    if (appFrameHostNames.ContainsKey(process.ProcessName))
                        continue;

                    var handle = FindWindowsStoreWindowHandle(process);
                    if (handle == IntPtr.Zero)
                        continue;

                    lock ("lock_LoadWindowsStoreProcess")
                    {
                        if (!windowsStoreWindowsHandles.ContainsKey(handle))
                            windowsStoreWindowsHandles.Add(handle, process);
                    }
                }
                catch (Exception) { }
            }
        }

        private static IntPtr FindWindowsStoreWindowHandle(Process process)
        {
            var isWindowsStoreApp = WindowsApi.IsWindowsStoreApp(process);  //IsImmersiveProcess(processHandle); //
            if (!isWindowsStoreApp)
                return IntPtr.Zero;


            bool found = false;
            var prevWindow = IntPtr.Zero;

            while (!found)
            {
                var desktopWindow = WindowsApi.GetDesktopWindow();
                if (desktopWindow == IntPtr.Zero)
                    break;

                var nextWindow = WindowsApi.FindWindowEx(desktopWindow, prevWindow, null, null);
                if (nextWindow == IntPtr.Zero)
                    break;

                // Check whether window belongs to the correct process.
                var nextWindowProcess = WindowsApi.GetProcess(nextWindow);

                if (nextWindowProcess == null)
                    break;

                if (nextWindowProcess.Id == process.Id)
                {
                    if (WindowsApi.IsWindowVisible(nextWindow)
                        && !WindowsApi.IsIconic(nextWindow)
                        && WindowsApi.GetParent(nextWindow) == IntPtr.Zero)
                        return nextWindow;
                    else
                    {
                        var parent = WindowsApi.GetParent(nextWindow);
                        var validParent = WindowsApi.IsWindowVisible(parent) && !WindowsApi.IsIconic(parent);
                        //&& GetProp(parent, "Windows.ImmersiveShell.IdentifyAsMainCoreWindow") > 0;
                        if (validParent)
                            return parent;
                    }
                }

                prevWindow = nextWindow;
            }

            return IntPtr.Zero;
        }

        private static List<IntPtr> GetChildWindows(IntPtr windowHandle)
        {
            var childWindows = WindowsApi.GetChildWindows(windowHandle);
            int tries = 1;
            while (!childWindows.Any())
            {
                childWindows = WindowsApi.GetChildWindows(windowHandle);
                Thread.Sleep(100);

                tries++;
                if (tries > 100)
                    return childWindows;
            }
            return childWindows;
        }

        private static void LoadRealProcess(IntPtr windowHandle)
        {
            // wait for background thread loading Windows Store processes
            if (loadingStoreProcess)
            {
                while (loadingStoreProcess)
                    Thread.Sleep(50);

                if (windowsStoreWindowsHandles.ContainsKey(windowHandle))
                    return;
            }


            var childWindows = WindowsApi.GetChildWindows(windowHandle);
            if (!childWindows.Any())
                return;

            var processes = childWindows.Select(i => WindowsApi.GetProcess(i))
                                        .GroupBy(p => p.Id)
                                        .Select(group => group.First())
                                        .ToList();

            LoadWindowsStoreProcess(processes);
        }
    }
}