using System.Drawing;
using WindowsTime.Properties;

namespace WindowsTime
{
    public class WindowsTimeIconeResource : IIconeResource
    {
        public Image AplicacaoWin32 { get { return SystemIcons.Application.ToBitmap(); } }
        public Image WindowsLogo { get { return Resources.windows.ToBitmap(); } }
    }
}