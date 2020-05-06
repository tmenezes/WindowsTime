using System.Diagnostics;
using System.Drawing;
using WindowsTime.Core.Monitorador.Extensions;
using WindowsTime.Core.Monitorador.Helpers;

namespace WindowsTime.Core.Monitorador
{
    public class ProgramaWin32 : Programa
    {
        private Image _icone;
        public override Image Icon { get { return _icone ?? (_icone = IconeHelper.GetIcone(this)); } }


        public ProgramaWin32(Process processo, Janela janela)
            : base(janela)
        {
            Tipo = TipoDePrograma.Win32;
            Processo = processo;

            Nome = processo.GetDescription() ?? ProcessHelper.POGRAMA_DESCONHECIDO;
            Executavel = processo.GetFileName();
        }
    }
}