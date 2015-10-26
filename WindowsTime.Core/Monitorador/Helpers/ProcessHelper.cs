using System.Diagnostics;

namespace WindowsTime.Core.Monitorador.Helpers
{
    public static class ProcessHelper
    {
        internal const string POGRAMA_DESCONHECIDO = "Windows";
        internal const string PROGRAMA_WINDOWS_EXPLORER = "Windows Explorer";

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
    }
}