using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace WindowsTime
{
    internal static class ProcessHelper
    {
        private const string FILENAME_DESCONHECIDO = "Windows";
        private const string FILENAME_EXPLORER = "Windows Explorer";


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

        public static Process GetWindowsExplorerProcess()
        {
            return GetProcess("explorer");
        }

        public static Process GetConsoleProcess()
        {
            return GetProcess("cmd");
        }


        public static string GetWindowExecutableFileName(Process process)
        {
            try
            {
                if (HasMainModule(process))
                    return process.MainModule.FileName;

                if (IsExplorerProcess(process))
                    return GetWindowsExplorerProcess().MainModule.FileName;

                if (IsConsoleProcess(process))
                    return GetConsoleFileName();


                return FILENAME_DESCONHECIDO;
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? FILENAME_EXPLORER
                           : FILENAME_DESCONHECIDO;
            }
        }

        public static string GetWindowExecutableDescription(Process process)
        {
            try
            {
                if (HasMainModule(process))
                {
                    return process.MainModule.FileVersionInfo.FileDescription ?? TryGetExecutableName(process);
                }

                return TryGetExecutableName(process);
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? FILENAME_EXPLORER
                           : FILENAME_DESCONHECIDO;
            }
        }


        public static Icon GetProcessIcon(Process process)
        {
            try
            {
                if (HasMainModule(process))
                    return WindowsApi.GetIcon(process.MainModule.FileName, 0, true);

                if (IsExplorerProcess(process))
                    return WindowsApi.GetExplorerIcon(0, true);

                if (IsConsoleProcess(process))
                    return WindowsApi.GetIcon(GetConsoleFileName(), 0, true);

                return null;
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
            return (process != null) && (process.Id == GetWindowsExplorerProcess().Id);
        }

        private static string TryGetExecutableName(Process process)
        {
            try
            {
                if (HasMainModule(process))
                {
                    var filename = process.MainModule.FileName;

                    return filename.Substring(filename.LastIndexOf('\\') + 1);
                }

                return process.ProcessName;
            }
            catch (Exception)
            {
                return "Windows";
            }
        }

        private static bool HasMainModule(Process process)
        {
            try
            {
                return process != null && process.MainModule != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsConsoleProcess(Process process)
        {
            return (process != null) && (process.ProcessName == "cmd");
        }

        private static string GetConsoleFileName()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return Path.Combine(systemPath, "cmd.exe");
        }
    }
}