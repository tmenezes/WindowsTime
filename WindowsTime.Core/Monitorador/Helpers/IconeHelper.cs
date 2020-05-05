using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using WindowsTime.Core.Monitorador.Extensions;

namespace WindowsTime.Core.Monitorador.Helpers
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
                       ? IconeResource.ProgramaWin32
                       : IconeResource.WindowsLogo;
        }

        public static Image GetIcone(Image logo, Color bgColor)
        {
            var iconeTemp = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid() + ".png");

            using (var bitmap = new Bitmap(logo, 32, 32))
            {
                bitmap.SetResolution(logo.HorizontalResolution, logo.VerticalResolution);

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(bgColor);
                    graphics.DrawImage(logo, 0, 0);

                    // salva nova imagem, recarrega e apaga
                    bitmap.Save(iconeTemp);
                    var newLogo = Image.FromFile(iconeTemp);
                    Task.Factory.StartNew(() => File.Delete(iconeTemp));

                    return newLogo;
                }
            }
        }
    }
}