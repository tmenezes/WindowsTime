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
        private Image _icon;
        public WindowsStorePackageId PackageId { get; private set; }
        public Package AppxPackage { get; private set; }

        public override Image Icon => _icon ?? (_icon = GetIcon());


        // construtor
        public ProgramaWindowsStore(Process processo, Janela janela)
            : base(janela)
        {
            Tipo = TipoDePrograma.WindowsStore;

            Processo = GetRealProcess(processo, janela);

            LoadProgramData();
        }


        private Process GetRealProcess(Process processo, Janela janela)
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(processo);
            if (!isFrameHost)
            {
                return processo;
            }

            Thread.Sleep(1000); // aguarda o Windows 10 mudar contexto da aplicação de AppFrameHost p/ a app real

            return WindowsStoreApi.GetWindowsStoreRealProcess(janela.WindowsHandle) ?? WindowsApi.GetProcess(janela.WindowsHandle);
        }
        private void LoadProgramData()
        {
            PackageId = WindowsStoreApi.GetWindowsStorePackageId(Processo);
            Executavel = Processo.GetFileName();

            AppxPackage = SerializationHelper.DeSerializeObject<Package>(GetAppxManifestPath()); ;
            Nome = GetName();
        }
        private string GetAppxManifestPath()
        {
            var possiblePaths = new[]
            {
                PackageId.InstalledFolder,
                Path.GetDirectoryName(Executavel),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "WindowsApps", PackageId.FullName),
            };

            foreach (var path in possiblePaths)
            {
                var appxManifestFile = Path.Combine(path, "AppxManifest.xml");
                if (File.Exists(appxManifestFile))
                    return appxManifestFile;
            }
            return null;
        }

        private string GetName()
        {
            string nomeDeExibicao;
            var nomeValido = TryGetNmeFromPackage(out nomeDeExibicao);
            if (nomeValido)
                return nomeDeExibicao;

            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);

            return (isFrameHost)
                ? PackageId.Name ?? "Windows Store App"
                : Processo.GetDescription() ?? PackageId.Name ?? PackageId.FullName ?? "Windows Store App";
        }
        private bool TryGetNmeFromPackage(out string nomeDeExibicao)
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

        private Image GetIcon()
        {
            var isFrameHost = WindowsStoreApi.IsFrameHostProcess(Processo);
            if (isFrameHost)
                return GetWindowsStoreIcon();

            var exeIcon = Processo?.GetIcon();
            if (exeIcon != null)
                return exeIcon.ToBitmap();

            return GetWindowsStoreIcon();
        }
        private Image GetWindowsStoreIcon()
        {
            if (AppxPackage == null)
                return IconeHelper.GetIcone(this);

            var app = AppxPackage.Applications.First();
            var appLogo = app.VisualElements.GetLogo();

            var possibleLogoPaths = new[]
            {
                Path.Combine(PackageId.InstalledFolder, appLogo),
                Path.Combine(Path.GetDirectoryName(Executavel), appLogo),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "WindowsApps", PackageId.FullName),
            };

            foreach (var logoPath in possibleLogoPaths)
                foreach (var subPasta in ConfiguracaoHelper.Logo.PastasDeContraste)
                {
                    var logoFile = Path.Combine(Path.GetDirectoryName(logoPath), subPasta, Path.GetFileName(appLogo));

                    foreach (var size in ConfiguracaoHelper.Logo.TamanhosAlvo)
                    {
                        var logoFileAlternativeSize = Path.ChangeExtension(logoFile, size);
                        if (!File.Exists(logoFileAlternativeSize))
                            continue;

                        var logo = Image.FromFile(logoFileAlternativeSize);
                        var bgColor = GetBackGroundColor(app);

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