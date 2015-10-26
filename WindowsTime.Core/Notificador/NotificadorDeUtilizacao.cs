using System;
using System.Linq;
using System.Timers;
using WindowsTime.Core.Dados;
using WindowsTime.Core.Monitorador;

namespace WindowsTime.Core.Notificador
{
    public class NotificadorDeUtilizacao
    {
        // atributos
        private const int INTERVALO = 1000 * 60 * 5; // 5 minutos
        private static NotificadorDeUtilizacao _instance;
        private readonly Timer _timer;

        // propriedades
        public static NotificadorDeUtilizacao Instance
        {
            get { return _instance ?? (_instance = new NotificadorDeUtilizacao()); }
        }


        // construtor
        private NotificadorDeUtilizacao()
        {
            _timer = new Timer
            {
                Interval = INTERVALO,
                Enabled = true
            };
            _timer.Elapsed += _timer_Elapsed;
        }


        // publicos
        internal void Iniciar()
        {
            if (Usuario.Corrente == null)
            {
                throw new InvalidOperationException("É necessário efetuar o login antes de iniciar o Notificador de utilização");
            }

            _timer.Start();
        }

        internal void Parar()
        {
            _timer.Stop();
        }


        // privados e etc
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var utilizacaoDeProgramas = ObterUtilizacaoDeProgramas();

            ClienteHttp.PostarUtilizacaoDeProgramas(utilizacaoDeProgramas);
        }

        private UtilizacaoDePrograma ObterUtilizacaoDeProgramas()
        {
            var programas = MonitoradorDeJanela.Instance.Janelas.Values
                                               .GroupBy(j => j.Programa.Nome)
                                               .Select(group => new DadosDoPrograma()
                                               {
                                                   Nome = group.Key,
                                                   TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividade.TotalSeconds),
                                                   Janelas = group.Select(i => new DadosDaJanela(i)),
                                                   //TotalJanelas = group.Sum(i => i.Programa.TotalDeAreasVisitadas),
                                                   //Icone = group.First().Programa.Icone,
                                               })
                                               .ToList();

            var utilizacaoDeProgramas = new UtilizacaoDePrograma(programas);

            return utilizacaoDeProgramas;
        }
    }
}
