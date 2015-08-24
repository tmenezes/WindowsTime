using System.Threading.Tasks;
using WindowsTime.Monitorador.Api;
using WindowsTime.Monitorador.Api.Helpers;

namespace WindowsTime.Monitorador
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