using System.Drawing;
using WindowsTime.Monitorador;
using WindowsTime.Properties;

namespace WindowsTime
{
    public class WindowsTimeIconeResource : IIconeResource
    {
        private readonly Image _programaWin32;
        private readonly Image _windowsLogo;

        public Image ProgramaWin32 { get { return _programaWin32; } }
        public Image WindowsLogo { get { return _windowsLogo; } }

        public WindowsTimeIconeResource()
        {
            _programaWin32 = SystemIcons.Application.ToBitmap();
            _windowsLogo = Resources.windows.ToBitmap();
        }
    }
}