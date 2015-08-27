using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using WindowsTime.Monitorador.Api;
using WindowsTime.Monitorador.Api.Structs;
using WindowsTime.Monitorador.Api.WindowsColor;
using WindowsTime.Monitorador.Extensions;
using WindowsTime.Monitorador.Helpers;

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
            var nomeArquivoLogo = aplicacao.VisualElements.GetLogo();
            var arquivoLogoBase = Path.Combine(PackageId.InstalledFolder, nomeArquivoLogo);

            foreach (var subPasta in ConfiguracaoHelper.Logo.PastasDeContraste)
            {
                var arquivoLogo = Path.Combine(Path.GetDirectoryName(arquivoLogoBase), subPasta, Path.GetFileName(nomeArquivoLogo));

                foreach (var tamanho in ConfiguracaoHelper.Logo.TamanhosAlvo)
                {
                    var arquivoLogoComTamanho = Path.ChangeExtension(arquivoLogo, tamanho);
                    if (!File.Exists(arquivoLogoComTamanho))
                        continue;

                    var logo = Image.FromFile(arquivoLogoComTamanho);
                    var bgColor = GetBackGroundColor(aplicacao);

                    return IconeHelper.GetIcone(logo, bgColor);
                }
            }

            return IconeHelper.GetIcone(this);
        }

        private Color GetBackGroundColor(Application appxApplication)
        {
            var bg = appxApplication.VisualElements.BackgroundColor;
            if (bg.ToLower() == "transparent")
                return ColorFunctions.GetImmersiveColor(ImmersiveColors.ImmersiveStartSelectionBackground);

            return (Color)new ColorConverter().ConvertFromString(appxApplication.VisualElements.BackgroundColor);
        }
    }
}