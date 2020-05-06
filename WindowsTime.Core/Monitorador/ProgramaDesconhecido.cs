using System.Drawing;
using WindowsTime.Core.Monitorador.Helpers;

namespace WindowsTime.Core.Monitorador
{
    public class ProgramaDesconhecido : Programa
    {
        public override Image Icon { get { return IconeHelper.IconeResource.WindowsLogo; } }

        public ProgramaDesconhecido(Janela janela)
            : base(janela)
        {
            Tipo = TipoDePrograma.Win32;
            Nome = ProcessHelper.POGRAMA_DESCONHECIDO;
            Executavel = ProcessHelper.POGRAMA_DESCONHECIDO;
        }
    }
}