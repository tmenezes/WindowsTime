using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public Package AppxPackage { get; private set; }

        public override Image Icone { get { return _icone ?? (_icone = GetIcone()); } }


        // construtor
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
                ? PackageId.InstalledFolder
                : Path.GetDirectoryName(Executavel);

            var arquivoAppxManifest = Path.Combine(pastaBase, "AppxManifest.xml");
            var pacoteAppxManifest = SerializationHelper.DeSerializeObject<Package>(arquivoAppxManifest);

            AppxPackage = pacoteAppxManifest;
            Nome = ObterNome();
        }

        private string ObterNome()
        {
            string nomeDeExibicao;
            var nomeValido = TentarObterNomeDoPacote(out nomeDeExibicao);
            if (nomeValido)
                return nomeDeExibicao;


            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            return (isFrameHost)
                ? PackageId.Name ?? "Windows Store App"
                : Processo.GetDescription() ?? PackageId.Name ?? PackageId.FullName ?? "Windows Store App";
        }
        private bool TentarObterNomeDoPacote(out string nomeDeExibicao)
        {
            nomeDeExibicao = (AppxPackage != null)
                ? AppxPackage.GetDisplayName()
                : string.Empty;

            var nomeValido = !string.IsNullOrEmpty(nomeDeExibicao);
            if (!nomeValido)
                return false;

            var nomeEhResource = nomeDeExibicao.ToLower().Contains("ms-resource");
            if (nomeEhResource)
            {
                var uri = new Uri(nomeDeExibicao);
                var chave = string.Format("ms-resource://{0}/resources/{1}", PackageId.Name, uri.Segments.Last());
                var nomeDeExibicaoPorResource = WindowsApi.GetResourceString(PackageId.ResourcesPriFilePath, chave);

                bool nomeDoResourceValido = !string.IsNullOrEmpty(nomeDeExibicaoPorResource);
                if (!nomeDoResourceValido)
                {
                    nomeDeExibicao = null;
                    return false;
                }

                nomeDeExibicao = nomeDeExibicaoPorResource;
                return true;
            }

            return true;
        }

        private Image GetIcone()
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            if (isFrameHost)
                return GetWindowsStoreIcone();

            var iconeDoExecutavel = (Processo != null) ? Processo.GetIcon() : null;
            if (iconeDoExecutavel != null)
                return iconeDoExecutavel.ToBitmap();

            return GetWindowsStoreIcone();
        }
        private Image GetWindowsStoreIcone()
        {
            if (AppxPackage == null)
                return IconeHelper.GetIcone(this);


            var aplicacao = AppxPackage.Applications.First();
            var targetSizes = new[] { "targetsize-32.png", ".png", "targetsize-48.png", "targetsize-256.png" };
            var arquivoLogo = Path.Combine(PackageId.InstalledFolder, aplicacao.VisualElements.Square30x30Logo);

            foreach (var size in targetSizes)
            {
                arquivoLogo = Path.ChangeExtension(arquivoLogo, size);
                if (!File.Exists(arquivoLogo))
                    continue;

                var logo = Image.FromFile(arquivoLogo);
                //var graphic = Graphics.FromImage(logo);
                //var bgColor = (Color)new ColorConverter().ConvertFromString(aplicacao.VisualElements.BackgroundColor);

                //graphic.Clear(bgColor); // altera o background
                //graphic.DrawImage(logo, new PointF(0, 0));
                return logo;
            }

            return IconeHelper.GetIcone(this);
        }
    }
}