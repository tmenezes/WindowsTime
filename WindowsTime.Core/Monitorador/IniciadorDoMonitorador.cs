using System.Threading.Tasks;
using WindowsTime.Core.Monitorador.Api;
using WindowsTime.Core.Monitorador.Helpers;

namespace WindowsTime.Core.Monitorador
{
    public class IniciadorDoMonitorador
    {
        private static readonly MonitoradorDeJanela monitoradorDeJanela = MonitoradorDeJanela.Instance;

        public static void Iniciar(IIconeResource iconeResource)
        {
            Task.Factory.StartNew(() => WindowsStoreApi.LoadWindowsStoreProcess());

            IconeHelper.IconeResource = iconeResource;

            monitoradorDeJanela.Iniciar();
        }

        public static void Parar()
        {
            monitoradorDeJanela.Parar();
        }
    }
}