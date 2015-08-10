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
                return process.Id == GetProcess("explorer").Id
                           ? "Windows Explorer"
                           : "ERRO";
            }
        }

        public static string GetWindowExecutableDescription(Process process)
        {
            try
            {
                return process.MainModule.FileVersionInfo.FileDescription;
            }
            catch (Exception)
            {
                return process.Id == GetProcess("explorer").Id
                           ? "Windows Explorer"
                           : "ERRO";
            }
        }

        public static Icon GetProcessIcon(int processId)
        {
            var process = Process.GetProcessById(processId);
            var ico = Icon.ExtractAssociatedIcon(process.MainModule.FileName);

            return ico;
        }
    }
}