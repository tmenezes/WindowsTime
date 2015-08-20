using System.Diagnostics;
using System.Drawing;
using WindowsTime.Monitorador.Api;
using WindowsTime.Monitorador.Api.Extensions;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador
{
    public class ProgramaWindowsStore : Programa
    {
        private Image _icone;
        public WindowsStorePackageId PackageId { get; private set; }

        public override Image Icone { get { return _icone ?? (_icone = IconeHelper.GetIcone(this)); } }


        public ProgramaWindowsStore(Process processo, string titulo) : base(processo, titulo)
        {
            Tipo = TipoDePrograma.WindowsStore;
            PackageId = WindowsApi.GetWindowsStorePackageId(processo);

            Nome = PackageId.Name;
            Executavel = processo.GetWindowExecutableFileName();
        }
    }
}