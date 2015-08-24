using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador.Api.Extensions
{
    internal static class ProcessExtensions
    {
        public static string GetFileName(this Process process)
        {
            try
            {
                if (HasMainModule(process))
                    return process.MainModule.FileName;

                if (IsExplorerProcess(process))
                    return ProcessHelper.GetWindowsExplorerProcess().MainModule.FileName;

                if (IsConsoleProcess(process))
                    return GetConsoleFileName();


                return ProcessHelper.POGRAMA_DESCONHECIDO;
            }
            catch (Exception)
            {
                return IsExplorerProcess(process)
                           ? ProcessHelper.PROGRAMA_WINDOWS_EXPLORER
                           : ProcessHelper.POGRAMA_DESCONHECIDO;
            }
        }

        public static string GetDescription(this Process process)
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
                           ? ProcessHelper.PROGRAMA_WINDOWS_EXPLORER
                           : null;
            }
        }

        public static Icon GetIcon(this Process process)
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

        public static bool IsExplorerProcess(this Process process)
        {
            return (process != null) && (process.Id == ProcessHelper.GetWindowsExplorerProcess().Id);
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