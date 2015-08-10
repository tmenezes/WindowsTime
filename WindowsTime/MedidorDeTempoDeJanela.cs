using System;
using System.Collections.Generic;
using System.Timers;

namespace WindowsTime
{
    public class MedidorDeTempoDeJanela
    {
        // atributos
        private const int INTERVALO = 100;
        private static MedidorDeTempoDeJanela _instance;
        private readonly Timer _timer;
        private Janela ultimaJanelaAtiva;

        // propriedades
        public Dictionary<int, Janela> Janelas { get; set; }
        public static MedidorDeTempoDeJanela Instance
        {
            get { return _instance ?? (_instance = new MedidorDeTempoDeJanela()); }
        }
        public Janela JanelaAtiva { get { return ultimaJanelaAtiva; } }

        // eventos
        public event EventHandler<Janela> NovaJanelaAtiva;


        // construtor
        private MedidorDeTempoDeJanela()
        {
            Janelas = new Dictionary<int, Janela>();

            _timer = new Timer
            {
                Interval = INTERVALO,
                Enabled = true
            };
            _timer.Elapsed += _timer_Elapsed;
        }


        // publicos
        public void Iniciar()
        {
            _timer.Start();
        }

        public void Parar()
        {
            _timer.Start();
        }


        // privados e etc
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var handle = WindowsApi.GetActiveWindowHandle();

            var janela = GetJanelaCorrente(handle);

            if (!janela.EstaAtiva)
            {
                janela.NotificarJanelaAtiva();
                OnNovaJanelaAtiva(janela);
            }

            var deveNofiticaQueUltimaJanelaFoiDesativada = (janela != ultimaJanelaAtiva) && (ultimaJanelaAtiva != null);
            if (deveNofiticaQueUltimaJanelaFoiDesativada)
                ultimaJanelaAtiva.NotificarJanelaInativa();

            ultimaJanelaAtiva = janela;
        }

        private Janela GetJanelaCorrente(int handle)
        {
            var janelaJaMonitorada = Janelas.ContainsKey(handle);

            var janela = (janelaJaMonitorada)
                             ? Janelas[handle]
                             : new Janela(handle);

            if (!janelaJaMonitorada)
                Janelas.Add(handle, janela);

            return janela;
        }

        protected virtual void OnNovaJanelaAtiva(Janela janela)
        {
            NovaJanelaAtiva?.Invoke(this, janela);
        }
    }
}