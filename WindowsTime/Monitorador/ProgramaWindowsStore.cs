using System.Diagnostics;
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


        public ProgramaWindowsStore(Process processo, Janela janela)
            : base(janela)
        {
            Processo = ObterProcessoReal(processo, janela);

            Tipo = TipoDePrograma.WindowsStore;
            PackageId = WindowsStoreApi.GetWindowsStorePackageId(Processo);

            Nome = ObterNome();
            Executavel = Processo.GetFileName();
        }


        private Process ObterProcessoReal(Process processo, Janela janela)
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(processo);
            if (!isFrameHost)
            {
                return processo;
            }


            Thread.Sleep(1000); // aguarda o Windows 10 mudar contexto da aplicação de AppFrameHost p/ a app real

            return WindowsStoreApi.GetWindowsStoreRealProcess(janela.WindowsHandle) ?? WindowsApi.GetProcess(janela.WindowsHandle);
        }

        private string ObterNome()
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            return (isFrameHost)
                ? PackageId.Name ?? "Windows Store App"
                : Processo.GetDescription() ?? PackageId.Name ?? PackageId.FullName ?? "Windows Store App";
        }
    }
}