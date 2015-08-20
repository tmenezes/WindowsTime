namespace WindowsTime
{
    public class IniciadorDoMonitorador
    {
        private static readonly MonitoradorDeJanela monitoradorDeJanela = MonitoradorDeJanela.Instance;

        public static void Iniciar(IIconeResource iconeResource)
        {
            IconeHelper.IconeResource = iconeResource;

            monitoradorDeJanela.Iniciar();
        }

        public static void Parar()
        {
            monitoradorDeJanela.Parar();
        }
    }
}