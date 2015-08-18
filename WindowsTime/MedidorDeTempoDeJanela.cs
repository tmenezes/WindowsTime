using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Win32;

namespace WindowsTime
{
    public class MedidorDeTempoDeJanela
    {
        // atributos
        private const int INTERVALO = 100;
        private static MedidorDeTempoDeJanela _instance;
        private readonly Timer _timer;
        private Janela _ultimaJanelaAtiva;
        private bool _maquinaLockada;

        // propriedades
        public Dictionary<int, Janela> Janelas { get; set; }
        public static MedidorDeTempoDeJanela Instance
        {
            get { return _instance ?? (_instance = new MedidorDeTempoDeJanela()); }
        }
        public Janela JanelaAtiva { get { return _ultimaJanelaAtiva; } }

        // eventos
        public event EventHandler<Janela> NovaJanelaAtiva;
        public event EventHandler<Janela> TempoMedido;


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
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            _timer.Start();
        }

        public void Parar()
        {
            _timer.Stop();

            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
        }


        // privados e etc
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_maquinaLockada)
                return;


            var handle = WindowsApi.GetActiveWindowHandle();

            var janela = GetJanelaCorrente(handle);

            if (!janela.EstaAtiva)
            {
                janela.NotificarJanelaAtiva();
                OnNovaJanelaAtiva(janela);
            }
            OnTempoMedido(janela);

            var trocouDeJanela = (janela != _ultimaJanelaAtiva) && (_ultimaJanelaAtiva != null);
            if (trocouDeJanela)
                _ultimaJanelaAtiva.NotificarJanelaInativa();

            var novoTitulo = WindowsApi.GetWindowsText(handle);
            var trocouDeTitulo = (janela == _ultimaJanelaAtiva) && janela.Titulo != novoTitulo;
            if (trocouDeTitulo)
                janela.NotificarMudancaDeTitulo(novoTitulo);

            _ultimaJanelaAtiva = janela;
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _maquinaLockada = true;

                if (_ultimaJanelaAtiva != null)
                    _ultimaJanelaAtiva.NotificarJanelaInativa();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _maquinaLockada = false;
            }
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
            var handler = NovaJanelaAtiva;
            if (handler != null) handler(this, janela);
        }
        protected virtual void OnTempoMedido(Janela e)
        {
            var handler = TempoMedido;
            if (handler != null) handler(this, e);
        }
    }
}