using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using WindowsTime.Monitorador.Api;
using WindowsTime.Monitorador.Api.Extensions;
using WindowsTime.Monitorador.Api.Helpers;
using WindowsTime.Monitorador.Api.Structs;

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
            Tipo = TipoDePrograma.WindowsStore;

            Processo = ObterProcessoReal(processo, janela);

            CarregarDadosDoPrograma();
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

        private void CarregarDadosDoPrograma()
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            PackageId = WindowsStoreApi.GetWindowsStorePackageId(Processo);
            Executavel = Processo.GetFileName();

            var pastaBase = (isFrameHost)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WindowsApps", PackageId.FullName)
                : Path.GetDirectoryName(Executavel);

            var arquivoAppxManifest = Path.Combine(pastaBase, "AppxManifest.xml");
            var pacoteAppxManifest = SerializationHelper.DeSerializeObject<Package>(arquivoAppxManifest);

            Nome = ObterNome(pacoteAppxManifest);
        }

        private string ObterNome(Package pacoteAppxManifest)
        {
            var displayNameValido = pacoteAppxManifest != null && !pacoteAppxManifest.GetDisplayName().ToLower().Contains("ms-resource");
            if (displayNameValido)
            {
                return pacoteAppxManifest.GetDisplayName();
            }

            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            return (isFrameHost)
                ? PackageId.Name ?? "Windows Store App"
                : Processo.GetDescription() ?? PackageId.Name ?? PackageId.FullName ?? "Windows Store App";
        }
    }
}