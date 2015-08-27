using System.Configuration;

namespace WindowsTime.Monitorador.Helpers
{
    internal static class ConfiguracaoHelper
    {
        internal static class Logo
        {
            static Logo()
            {
                var configPastaDeContraste = ConfigurationManager.AppSettings["Logo.PastasDeContraste"];

                PastasDeContraste = (string.IsNullOrEmpty(configPastaDeContraste))
                    ? new[] { "", "contrast-black" }
                    : configPastaDeContraste.Split(',');


                var configTamanhosAlvo = ConfigurationManager.AppSettings["Logo.TamanhosAlvo"];

                TamanhosAlvo = (string.IsNullOrEmpty(configTamanhosAlvo))
                    ? new[] { ".png",
                              "targetsize-32.png", "scale-100.png", 
                              "targetsize-36.png", "targetsize-40.png", 
                              "contrast-black_targetsize-32.png", "contrast-black_scale-100.png", 
                              "contrast-black_targetsize-36.png", "contrast-black_targetsize-40.png", 
                              "contrast-black_scale-125.png", "contrast-black_scale-150.png", 
                              "scale-125.png", "scale-150.png", 
                              "targetsize-48.png", "targetsize-256.png" }
                    : configTamanhosAlvo.Split(',');
            }

            internal static string[] PastasDeContraste { get; private set; }
            internal static string[] TamanhosAlvo { get; private set; }
        }
    }
}
