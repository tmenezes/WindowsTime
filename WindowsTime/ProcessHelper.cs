using System;
using System.Diagnostics;
using System.Drawing;

namespace WindowsTime
{
    internal static class ProcessHelper
    {
        public static Process GetProcess(int id)
        {
            var process = Process.GetProcessById(id);

            return process;
        }

        public static Process GetProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);

            return (processes.Length > 0)
                       ? processes[0]
                       : null;
        }

        public static string GetWindowExecutableFileName(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? "Windows Explorer"
                           : "Windows";
            }
        }

        public static string GetWindowExecutableDescription(Process process)
        {
            try
            {
                return process.MainModule.FileVersionInfo.FileDescription ?? TryGetExecutableName(process);
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? "Windows Explorer"
                           : "Windows";
            }
        }

        public static Icon GetProcessIcon(Process process)
        {
            try
            {
                return WindowsApi.GetIcon(process.MainModule.FileName, 0, true);
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? WindowsApi.GetExplorerIcon(0, true)
                           : null;
            }
        }


        private static bool IsExplorerProcess(Process process)
        {
            return (process != null) && (process.Id == GetProcess("explorer").Id);
        }

        private static string TryGetExecutableName(Process process)
        {
            try
            {
                var filename = process.MainModule.FileName;

                return filename.Substring(filename.LastIndexOf('\\') + 1);
            }
            catch (Exception)
            {
                return "Windows";
            }
        }
    }
}