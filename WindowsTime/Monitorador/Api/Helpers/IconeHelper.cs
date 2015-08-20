using System;
using System.Drawing;
using WindowsTime.Monitorador.Api.Extensions;

namespace WindowsTime.Monitorador.Api.Helpers
{
    public static class IconeHelper
    {
        public static IIconeResource IconeResource { get; set; }

        public static Image GetIcone(Programa programa)
        {
            if (IconeResource == null)
                throw new InvalidOperationException("IconeResource inválido");


            if (programa.Processo == null)
                return IconeResource.WindowsLogo;

            var icone = programa.Processo.GetIcon();

            if (icone != null)
                return icone.ToBitmap();

            return programa.Tipo == TipoDePrograma.Win32
                       ? IconeResource.AplicacaoWin32
                       : IconeResource.WindowsLogo;
        }
    }
}