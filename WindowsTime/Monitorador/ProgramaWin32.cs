using System.Diagnostics;
using System.Drawing;
using WindowsTime.Monitorador.Api.Extensions;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador
{
    public class ProgramaWin32 : Programa
    {
        private Image _icone;
        public override Image Icone { get { return _icone ?? (_icone = IconeHelper.GetIcone(this)); } }


        public ProgramaWin32(Process processo, string titulo) : base(processo, titulo)
        {
            Tipo = TipoDePrograma.Win32;
            Nome = processo.GetWindowExecutableDescription();
            Executavel = processo.GetWindowExecutableFileName();
        }
    }
}