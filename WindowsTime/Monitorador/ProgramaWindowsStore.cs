using System.Drawing;
using System.Threading;
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


        public ProgramaWindowsStore(Janela janela)
            : base(janela)
        {
            Thread.Sleep(1000);
            Processo = WindowsStoreApi.GetWindowsStoreRealProcess(janela.WindowsHandle) ?? WindowsApi.GetProcess(janela.WindowsHandle);

            Tipo = TipoDePrograma.WindowsStore;
            PackageId = WindowsStoreApi.GetWindowsStorePackageId(Processo);

            Nome = Processo.GetDescription() ?? PackageId.Name ?? PackageId.FullName ?? "Windows Store App";
            Executavel = Processo.GetFileName();
        }
    }
}