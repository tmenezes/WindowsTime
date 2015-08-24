using System.Diagnostics;
using System.Drawing;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador
{
    public class ProgramaDesconhecido : Programa
    {
        public override Image Icone { get { return IconeHelper.IconeResource.WindowsLogo; } }

        public ProgramaDesconhecido(Janela janela)
            : base(janela)
        {
            Tipo = TipoDePrograma.Win32;
            Nome = ProcessHelper.POGRAMA_DESCONHECIDO;
            Executavel = ProcessHelper.POGRAMA_DESCONHECIDO;
        }
    }
}