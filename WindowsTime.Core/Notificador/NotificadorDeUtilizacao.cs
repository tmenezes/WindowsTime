using System;
using System.Linq;
using System.Timers;
using WindowsTime.Core.DTO;
using WindowsTime.Core.Monitorador;
using WindowsTime.Dominio;

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

        private UtilizacaoDTO ObterUtilizacaoDeProgramas()
        {
            var programas = MonitoradorDeJanela.Instance.Janelas.Values
                                               .Where(i => i.PendenteDeSincronizacao)
                                               .GroupBy(j => j.Programa.Nome)
                                               .Select(group => new ProgramaDTO()
                                               {
                                                   Nome = group.Key,
                                                   TempoDeUtilizacao = group.Sum(i => i.TempoDeAtividadeTotal.TotalSeconds),
                                                   TempoNaoSincronizado = group.Sum(i => i.CalcularTempoDeAtividadeNaoSincronizado().TotalSeconds),
                                                   Janelas = group.Select(i => new JanelaDTO(i)),
                                                   //Icone = group.First().Programa.Icone,
                                               })
                                               .ToList();

            var utilizacaoDeProgramas = new UtilizacaoDTO(programas);

            return utilizacaoDeProgramas;
        }
    }
}
