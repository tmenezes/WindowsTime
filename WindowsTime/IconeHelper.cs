using System;
using System.Drawing;

namespace WindowsTime
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

            return programa.Win32App
                       ? IconeResource.AplicacaoWin32
                       : IconeResource.WindowsLogo;
        }
    }
}