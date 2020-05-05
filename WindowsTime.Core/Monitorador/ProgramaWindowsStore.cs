using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using WindowsTime.Core.Monitorador.Api;
using WindowsTime.Core.Monitorador.Api.Structs;
using WindowsTime.Core.Monitorador.Api.WindowsColor;
using WindowsTime.Core.Monitorador.Extensions;
using WindowsTime.Core.Monitorador.Helpers;

namespace WindowsTime.Core.Monitorador
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
                var chave = $"ms-resource://{PackageId.Name}/resources/{uri.Segments.Last()}";
                var possibleResourcePaths = new[]
                {
                    GetResourcesPath(PackageId.ResourcesPriFilePath),
                    GetResourcesPath(Path.GetDirectoryName(Executavel)),
                };

                foreach (var resourcePath in possibleResourcePaths)
                {
                    var nomeDeExibicaoPorResource = WindowsApi.GetResourceString(resourcePath, chave);

                    var nomeDoResourceValido = !string.IsNullOrEmpty(nomeDeExibicaoPorResource);
                    if (nomeDoResourceValido)
                    {
                        nomeDeExibicao = nomeDeExibicaoPorResource;
                        return true;
                    }
                }

                nomeDeExibicao = null;
                return false;
            }

            return true;
        }
        private string GetResourcesPath(string basePath)
        {
            var containsBasePath = PackageId.ResourcesPriFilePath.Contains(basePath);
            if (containsBasePath)
                return basePath;

            var containsInstallationFolder = PackageId.ResourcesPriFilePath.Contains(PackageId.InstalledFolder);
            if (containsInstallationFolder)
                return PackageId.ResourcesPriFilePath.Replace(PackageId.InstalledFolder, basePath);

            return Path.Combine(basePath, PackageId.ResourcesPriFilePath);
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
            var possibleLogoPaths = new[]
            {
                Path.Combine(PackageId.InstalledFolder, nomeArquivoLogo),
                Path.Combine(Path.GetDirectoryName(Executavel), nomeArquivoLogo),
            };

            foreach (var logoPath in possibleLogoPaths)
                foreach (var subPasta in ConfiguracaoHelper.Logo.PastasDeContraste)
                {
                    var arquivoLogo = Path.Combine(Path.GetDirectoryName(logoPath), subPasta, Path.GetFileName(nomeArquivoLogo));

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